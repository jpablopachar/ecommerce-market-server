using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Errors;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher,
        IGenericSecurityRepository<User> securityRepository,
        RoleManager<IdentityRole> roleManager) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IGenericSecurityRepository<User> _securityRepository = securityRepository;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        /// <summary>
        /// Realiza el proceso de autenticación de un usuario utilizando sus credenciales.
        /// </summary>
        /// <param name="loginDto">Objeto que contiene el correo electrónico y la contraseña del usuario.</param>
        /// <returns>
        /// Un objeto <see cref="UserDto"/> con la información del usuario autenticado y su token de acceso,
        /// o una respuesta de error si la autenticación falla.
        /// </returns>
        /// <exception cref="UnauthorizedResult">
        /// Se retorna cuando el usuario no existe o las credenciales son incorrectas.
        /// </exception>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email!);

            if (user == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, roles),
                Name = user.Name,
                LastName = user.LastName,
                Image = user.Image,
                Admin = roles.Contains("Admin"),
            };
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema utilizando la información proporcionada.
        /// </summary>
        /// <param name="registerDto">Objeto que contiene los datos necesarios para el registro del usuario.</param>
        /// <returns>
        /// Un objeto <see cref="UserDto"/> con la información del usuario registrado y su token de acceso,
        /// o una respuesta de error si el registro falla.
        /// </returns>
        /// <exception cref="CodeErrorException">
        /// Se lanza cuando la creación del usuario falla debido a errores de validación o restricciones del sistema.
        /// </exception>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                Name = registerDto.Name,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password!);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorException(400));
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Admin = false,
                Token = _tokenService.CreateToken(user, null)
            };
        }

        /// <summary>
        /// Obtiene la información del usuario autenticado.
        /// </summary>
        /// <returns>
        /// Un objeto <see cref="UserDto"/> con la información del usuario autenticado,
        /// o una respuesta de error si la autenticación falla.
        /// </returns>
        /// <exception cref="UnauthorizedResult">
        /// Se retorna cuando el usuario no está autenticado.
        /// </exception>
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<UserDto>> Update(string id, RegisterDto registerDto)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe."));
            }

            user.Name = registerDto.Name;
            user.LastName = registerDto.LastName;
            user.Image = registerDto.Image;

            if (!string.IsNullOrEmpty(registerDto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400, "Error al actualizar la contraseña."));
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, roles),
                Image = user.Image,
                Admin = roles.Contains("Admin"),
            };
        }

        /// <summary>
        /// Obtiene la información de un usuario específico por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a buscar.</param>
        /// <returns>
        /// Un objeto <see cref="UserDto"/> con la información del usuario encontrado,
        /// o una respuesta de error si el usuario no existe.
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("pagination")]
        public async Task<ActionResult<Pagination<UserDto>>> GetUsers([FromQuery] UserSpecificationParams userParams)
        {
            var spec = new UserSpecification(userParams);

            var users = await _securityRepository.GetAllWithSpec(spec);

            var specCount = new UserForCountingSpecification(userParams);

            var totalUsers = await _securityRepository.CountAsync(specCount);

            var rounded = Math.Ceiling((double)totalUsers / userParams.PageSize);
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<User>, IReadOnlyList<UserDto>>(users);

            return Ok(new Pagination<UserDto>
            {
                Count = totalUsers,
                Data = data,
                PageCount = totalPages,
                PageIndex = userParams.PageIndex,
                PageSize = userParams.PageSize,
            });
        }

        /// <summary>
        /// Obtiene la información de un usuario específico por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a buscar.</param>
        /// <returns>
        /// Un objeto <see cref="UserDto"/> con la información del usuario encontrado,
        /// o una respuesta de error si el usuario no existe.
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpPut("role/{id}")]
        public async Task<ActionResult<UserDto>> UpdateRole(string id, RoleDto roleParam)
        {
            var role = await _roleManager.FindByNameAsync(roleParam.Name!);

            if (role == null)
            {
                return NotFound(new CodeErrorResponse(404, "El rol no existe."));
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe."));
            }

            var userDto = _mapper.Map<User, UserDto>(user);

            if (roleParam.Status)
            {
                var result = await _userManager.AddToRoleAsync(user, roleParam.Name!);

                if (result.Succeeded)
                {
                    userDto.Admin = true;
                }

                if (result.Errors.Any())
                {
                    if (result.Errors.Where(x => x.Code == "UserAlreadyInRole").Any())
                    {
                        userDto.Admin = true;
                    }
                }
            }
            else
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleParam.Name!);

                if (result.Succeeded)
                {
                    userDto.Admin = false;
                }
            }

            if (userDto.Admin)
            {
                var roles = new List<string>();

                roles.Add("ADMIN");

                userDto.Token = _tokenService.CreateToken(user, roles);
            }
            else
            {
                userDto.Token = _tokenService.CreateToken(user, null);
            }

            return userDto;
        }

        /// <summary>
        /// Obtiene la información de un usuario específico por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a buscar.</param>
        /// <returns>
        /// Un objeto <see cref="UserDto"/> con la información del usuario encontrado,
        /// o una respuesta de error si el usuario no existe.
        /// </returns>
        [Authorize(Roles = "ADMIN")]
        [HttpGet("account/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe."));
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Image = user.Image,
                Admin = roles.Contains("Admin"),
            };
        }

        /// <summary>
        /// Obtiene la información del usuario autenticado.
        /// </summary>
        /// <returns>
        /// Un objeto <see cref="UserDto"/> con la información del usuario autenticado,
        /// o una respuesta de error si la autenticación falla.
        /// </returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            var user = await _userManager.SearchUserAsync(HttpContext.User);

            var roles = await _userManager.GetRolesAsync(user!);

            return new UserDto
            {
                Id = user!.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Image = user.Image,
                Admin = roles.Contains("Admin"),
                Token = _tokenService.CreateToken(user, roles),
            };
        }

        /// <summary>
        /// Valida si un correo electrónico ya está registrado en el sistema.
        /// </summary>
        /// <param name="email">El correo electrónico a validar.</param>
        /// <returns>
        /// Un valor booleano que indica si el correo electrónico ya está registrado.
        /// </returns>
        [HttpGet("validEmail")]
        public async Task<ActionResult<bool>> ValidateEmail([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return false;

            return true;
        }

        /// <summary>
        /// Obtiene la dirección del usuario autenticado.
        /// </summary>
        /// <returns>
        /// Un objeto <see cref="AddressDto"/> con la información de la dirección del usuario,
        /// o una respuesta de error si el usuario no tiene una dirección asociada.
        /// </returns>
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var user = await _userManager.SearchUserWithAddressAsync(HttpContext.User);

            return _mapper.Map<Address, AddressDto>(user?.Address!);
        }

        /// <summary>
        /// Actualiza la dirección del usuario autenticado.
        /// </summary>
        /// <param name="address">La nueva dirección del usuario.</param>
        /// <returns>
        /// Un objeto <see cref="AddressDto"/> con la información de la dirección actualizada,
        /// o una respuesta de error si la actualización falla.
        /// </returns>
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            var user = await _userManager.SearchUserWithAddressAsync(HttpContext.User);

            user!.Address = _mapper.Map<AddressDto, Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("No se pudo actualizar la dirección del usuario");
        }
    }
}