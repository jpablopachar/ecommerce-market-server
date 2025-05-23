using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyCartController(IBuyCartRepository buyCartRepository) : ControllerBase
    {
        private readonly IBuyCartRepository _buyCartRepository = buyCartRepository;

        /// <summary>
        /// Obtiene el carrito de compras del usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del carrito de compras.</param>
        /// <returns>El carrito de compras correspondiente al ID.</returns>
        [HttpGet]
        public async Task<ActionResult<BuyCart>> GetBuyCartById(string id)
        {
            var buyCart = await _buyCartRepository.GetBuyCartByIdAsync(id);

            return Ok(buyCart ?? new BuyCart(id));
        }

        /// <summary>
        /// Actualiza el carrito de compras del usuario.
        /// </summary>
        /// <param name="buyCartParams">Los parámetros del carrito de compras a actualizar.</param>
        /// <returns>El carrito de compras actualizado.</returns>
        [HttpPost]
        public async Task<ActionResult<BuyCart>> UpdateBuyCart(BuyCart buyCartParams)
        {
            var updatedBuyCart = await _buyCartRepository.UpdateBuyCartAsync(buyCartParams);

            return Ok(updatedBuyCart);
        }

        /// <summary>
        /// Elimina el carrito de compras del usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del carrito de compras.</param>
        /// <returns>Un valor booleano que indica si la eliminación fue exitosa.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBuyCart(string id)
        {
            var result = await _buyCartRepository.DeleteBuyCartAsync(id);

            return Ok(result);
        }
    }
}