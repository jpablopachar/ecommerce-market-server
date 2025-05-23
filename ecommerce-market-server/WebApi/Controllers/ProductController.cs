using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IGenericRepository<Product> productRepository, IMapper mapper) : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Obtiene una lista paginada de productos.
        /// </summary>
        /// <param name="productoParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecificationParams productoParams)
        {
            var spec = new ProductWithCategoryAndBrandSpecification(productoParams);

            var products = await _productRepository.GetAllWithSpec(spec);

            var specCount = new ProductForCountingSpecification(productoParams);

            var totalProducts = await _productRepository.CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(productoParams.PageSize));
            var totalPages = Convert.ToInt32(rounded);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(
                new Pagination<ProductDto>
                {
                    Count = totalProducts,
                    PageIndex = productoParams.PageIndex,
                    PageSize = productoParams.PageSize,
                    PageCount = totalPages,
                    Data = data
                }
            );
        }

        /// <summary>
        /// Obtiene un producto específico por su ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductWithCategoryAndBrandSpecification(id);

            var product = await _productRepository.GetByIdWithSpec(spec);

            if (product == null) return NotFound(new CodeErrorResponse(404, "El producto no fue encontrado."));

            var productDto = _mapper.Map<Product, ProductDto>(product);

            return Ok(productDto);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            var result = await _productRepository.Add(product);

            if (result == 0)
            {
                throw new Exception("No se inserto el producto");
            }

            return Ok(product);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, Product product)
        {
            product.Id = id;

            var result = await _productRepository.Update(product);

            if (result == 0)
            {
                throw new Exception("No se pudo actualizar el producto");
            }

            return Ok(product);
        }
    }
}