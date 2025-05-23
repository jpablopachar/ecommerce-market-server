using System.Security.Claims;
using AutoMapper;
using Core.Entities.PurchaseOrder;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IMapper mapper) : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService = purchaseOrderService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Crea una nueva orden de compra.
        /// </summary>
        /// <param name="purchaseOrderDto">Los detalles de la orden de compra a crear.</param>
        /// <returns>La orden de compra creada.</returns>
        [HttpPost]
        public async Task<ActionResult<PurchaseOrderResponseDto>> AddPurchaseOrder(PurchaseOrderDto purchaseOrderDto)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var address = _mapper.Map<AddressDto, Core.Entities.PurchaseOrder.Address>(purchaseOrderDto.MailingAddress!);

            var purchaseOrder = await _purchaseOrderService.AddPurchaseOrderAsync(email!, purchaseOrderDto.ShippingType, purchaseOrderDto.BuyCartId!, address);

            if (purchaseOrder == null) return BadRequest(new CodeErrorResponse(400, "No se pudo crear la orden de compra."));

            var purchaseOrderResponse = _mapper.Map<PurchaseOrder, PurchaseOrderResponseDto>(purchaseOrder);

            return Ok(purchaseOrderResponse);
        }

        /// <summary>
        /// Obtiene todas las órdenes de compra del usuario autenticado.
        /// </summary>
        /// <returns>Una lista de órdenes de compra.</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PurchaseOrderResponseDto>>> GetPurchaseOrders()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersByEmailAsync(email!);

            var purchaseOrderResponses = _mapper.Map<IReadOnlyList<PurchaseOrder>, IReadOnlyList<PurchaseOrderResponseDto>>(purchaseOrders);

            return Ok(purchaseOrderResponses);
        }

        /// <summary>
        /// Obtiene una orden de compra específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la orden de compra.</param>
        /// <returns>La orden de compra solicitada.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderResponseDto>> GetPurchaseOrderById(int id)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id, email!);

            if (purchaseOrder == null) return NotFound(new CodeErrorResponse(404, "Orden de compra no encontrada."));

            var purchaseOrderResponse = _mapper.Map<PurchaseOrder, PurchaseOrderResponseDto>(purchaseOrder);

            return Ok(purchaseOrderResponse);
        }

        /// <summary>
        /// Obtiene todos los tipos de envío disponibles.
        /// </summary>
        /// <returns>Una lista de tipos de envío.</returns>
        [HttpGet("shippingType")]
        public async Task<ActionResult<IReadOnlyList<ShippingType>>> GetShippingTypes()
        {
            var shippingTypes = await _purchaseOrderService.GetShippingType();

            return Ok(shippingTypes);
        }
    }
}