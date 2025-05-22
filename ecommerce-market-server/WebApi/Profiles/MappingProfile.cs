using AutoMapper;
using Core.Entities;
using Core.Entities.PurchaseOrder;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    /// <summary>
    /// Define las configuraciones de mapeo de objetos entre entidades del dominio y DTOs utilizando AutoMapper.
    /// </summary>
    /// <remarks>
    /// Esta clase centraliza todas las reglas de conversión necesarias para transformar entidades del dominio en objetos de transferencia de datos (DTOs) y viceversa,
    /// facilitando la comunicación entre las capas de la aplicación y asegurando la consistencia de los datos expuestos por la API.
    /// </remarks>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.CategoryName, x => x.MapFrom(c => c.Category!.Name))
                .ForMember(p => p.BrandName, x => x.MapFrom(c => c.Brand!.Name));

            CreateMap<Core.Entities.Address, AddressDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<AddressDto, Core.Entities.PurchaseOrder.Address>();

            CreateMap<PurchaseOrder, PurchaseOrderResponseDto>()
                .ForMember(po => po.ShippingType, x => x.MapFrom(y => y.ShippingType!.Name))
                .ForMember(po => po.ShippingTypePrice, x => x.MapFrom(y => y.ShippingType!.Price));

            CreateMap<ItemOrder, ItemOrderResponseDto>()
                .ForMember(io => io.ProductId, x => x.MapFrom(y => y.OrderedItem!.ItemProductId))
                .ForMember(io => io.ProductName, x => x.MapFrom(y => y.OrderedItem!.ProductName))
                .ForMember(io => io.ProductImage, x => x.MapFrom(y => y.OrderedItem!.ImageUrl));
        }
    }
}