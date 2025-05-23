using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IGenericRepository<Category> categoryRepository) : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepository = categoryRepository;

        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        /// <returns>Una lista de todas las categorías.</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return Ok(categories);
        }

        /// <summary>
        /// Obtiene una categoría específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la categoría.</param>
        /// <returns>La categoría correspondiente al ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            return Ok(category);
        }
    }
}