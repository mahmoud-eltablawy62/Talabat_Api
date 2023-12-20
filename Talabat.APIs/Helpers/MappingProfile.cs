using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
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
            //var address = _mapper.Map <AddressDto,Address>(order.Addres)
            //CreateMap<AddressDto, Address>().ReverseMap();

            //CreateMap<Adress, AddressDto>().ReverseMap();

            //CreateMap<Core.Entities.Identity.Adress, AddressDto>();

            CreateMap<AddressDto,Address>();
            CreateMap<Core.Entities.Identity.Adress,AddressDto>().ReverseMap();


            CreateMap<CustomerBasketDto , CustomerBasket >();

            CreateMap<BasketItemDto, BasketItem>();

            
            CreateMap<Orders, OrderToReturnDto>().
                ForMember(d => d.DelievryType, S => S.MapFrom(S => S.DelievryType.Name)).
                ForMember(d => d.Cost, s => s.MapFrom(s => s.DelievryType.Cost));
                
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.Prod_Name, s => s.MapFrom(s => s.Prod.Prod_Name))
                .ForMember(d => d.Prod_Id, s => s.MapFrom(s => s.Prod.Id))
                .ForMember(d => d.Picture_Url, s => s.MapFrom(s => s.Prod.Picture_Url))
                .ForMember(d => d.Picture_Url, s => s.MapFrom<ItemResolver>());
                ;
        }
    }
}
