using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(S => S.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(S => S.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<CustomerBasketDto , CustomerBasket >();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Address>();
        }
    }
}
