using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity.Oreder_Aggregate
{
     public class OrderItem : BaseEntitiy
    {
        public OrderItem() { }

        public OrderItem(ProductItemOrder product, decimal price, int quantity)
        {
            Prod = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder Prod { get; set; }   
        public decimal Price { get; set; }    
        public int Quantity { get; set; }
    }
}
