using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.Core.Entities.Identity.Oreder_Aggregate
{
    public class Orders : BaseEntitiy
    {
        public Orders() { } 
        public Orders(string buyerEmail, Address address, DelievryType delievryType, ICollection<OrderItem> items, decimal sub_Collection)
        {
            BuyerEmail = buyerEmail;
            Address = address;
            DelievryType = delievryType;
            Items = items;
            Sub_Collection = sub_Collection;
        }

        public string BuyerEmail { set; get; }  
        public  DateTimeOffset DateTime { set; get; }   = DateTimeOffset.UtcNow;
        public OrderStatus Status { set; get; } = OrderStatus.Pending;
        public Address Address { set; get; }
        //public int DelievryTypeId { get; set; }
        public DelievryType DelievryType { set; get;}
        public ICollection<OrderItem> Items { set; get;}  = new HashSet<OrderItem>();   
        public decimal Sub_Collection { get; set; }
        public decimal Total_Collection() => Sub_Collection + DelievryType.Cost ;
        public string PaymentId { set; get; } = "";

    }
}
