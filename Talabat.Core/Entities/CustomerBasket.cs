using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Talabat.Core.Entities
{
     public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }  
        public List<BasketItem> Items { get; set; } 
        
        public decimal ShippingPrice { get; set; }
        public int? DelievryMethodId { get; set; }

        public string Payment_Id { get; set; }
        public string Secret_Name { get; set; } 

    }
}
