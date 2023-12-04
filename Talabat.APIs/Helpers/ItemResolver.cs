using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class ItemResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {

        private readonly IConfiguration _Configuration;
        public ItemResolver(IConfiguration configuration)
        {

            _Configuration = configuration;

        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Prod.Picture_Url))
            {
                return $"{_Configuration["ApiBaseUrl"]}/{source.Prod.Picture_Url}";
            }
            return string.Empty;
        }
    }
}
