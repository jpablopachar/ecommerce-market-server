using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IGenericRepository<Brand> brandRepository) : ControllerBase
    {
        private readonly IGenericRepository<Brand> _brandRepository = brandRepository;

        /// <summary>
        /// Obtiene una lista de todas las marcas.
        /// </summary>
        /// <returns>Una lista de marcas.</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetAllBrands()
        {
            return Ok(await _brandRepository.GetAllAsync());
        }

        /// <summary>
        /// Obtiene una marca por su ID.
        /// </summary>
        /// <param name="id">ID de la marca a buscar.</param>
        /// <returns>La marca encontrada o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand?>> GetBrandById(int id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }
    }
}