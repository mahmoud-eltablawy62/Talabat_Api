using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.APIs.Dtos
{
    public class OrderToReturnDto
    {
        public string BuyerEmail {set; get;}
        public DateTimeOffset DateTime {set; get;}
        public string Status { set; get; } 
        public Address Address { set; get; }       
        public string DelievryType { set; get; }
        public decimal Cost { set; get; }    
        public ICollection<OrderItemDto> Items { set; get; } = new HashSet<OrderItemDto>();
        public decimal Sub_Collection { get; set; }
        public decimal Total_Collection { set; get; }
        public string PaymentId { set; get; } 
    }
}
