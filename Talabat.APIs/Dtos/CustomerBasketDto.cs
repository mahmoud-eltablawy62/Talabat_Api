using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
       
        public decimal ShippingPrice { get; set; }
        public int DelievryMethodId { get; set; }

        public string? Payment_Id { get; set; }
        public string? Secret_Name { get; set; }
    }
}
