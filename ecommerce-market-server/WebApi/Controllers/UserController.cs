using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Errors;

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
    }
}