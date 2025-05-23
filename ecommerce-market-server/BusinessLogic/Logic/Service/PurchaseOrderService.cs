using Core.Entities;
using Core.Entities.PurchaseOrder;
using Core.Interfaces;
using Core.Specifications;

namespace BusinessLogic.Logic.Service
{
    /// <summary>
    /// Implementación del servicio para gestionar órdenes de compra en el sistema.
    /// </summary>
    /// <remarks>
    /// Esta clase proporciona métodos para crear nuevas órdenes de compra, obtener órdenes existentes
    /// y recuperar los tipos de envío disponibles.
    /// </remarks>
    /// <param name="buyCartRepository">Repositorio para gestionar carritos de compra.</param>
    public class PurchaseOrderService(IBuyCartRepository buyCartRepository, IUnitOfWork unitOfWork) : IPurchaseOrderService
    {
        private readonly IBuyCartRepository _buyCartRepository = buyCartRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PurchaseOrder?> AddPurchaseOrderAsync(string buyerEmail, int shippingType, string cartId, Core.Entities.PurchaseOrder.Address address)
        {
            var buyCart = await _buyCartRepository.GetBuyCartByIdAsync(cartId);
            var items = new List<ItemOrder>();

            foreach (var item in buyCart?.Items!)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var itemOrdered = new OrderedItemProduct(productItem!.Id, productItem.Name, productItem.Image);

                var itemOrder = new ItemOrder(itemOrdered, productItem.Price, item.Cant);

                items.Add(itemOrder);
            }

            var shippingTypeEntity = await _unitOfWork.Repository<ShippingType>().GetByIdAsync(shippingType);

            var subtotal = items.Sum(i => i.Price * i.Quantity);

            var purchaseOrder = new PurchaseOrder(buyerEmail, address, shippingTypeEntity, items, subtotal);

            _unitOfWork.Repository<PurchaseOrder>().AddEntity(purchaseOrder);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                return null;
            }

            await _buyCartRepository.DeleteBuyCartAsync(cartId);

            return purchaseOrder;
        }

        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id, string email)
        {
            var spec = new PurchaseOrderWithItemsSpecifications(id, email);

            return await _unitOfWork.Repository<PurchaseOrder>().GetByIdWithSpec(spec);
        }

        public async Task<IReadOnlyList<PurchaseOrder>> GetPurchaseOrdersByEmailAsync(string email)
        {
            var spec = new PurchaseOrderWithItemsSpecifications(email);

            return await _unitOfWork.Repository<PurchaseOrder>().GetAllWithSpec(spec);
        }

        public async Task<IReadOnlyList<ShippingType>> GetShippingType()
        {
            return await _unitOfWork.Repository<ShippingType>().GetAllAsync();
        }
    }
}