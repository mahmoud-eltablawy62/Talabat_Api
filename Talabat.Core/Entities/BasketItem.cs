using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public string Picture_Url { get; set; } 
        public decimal Price { get; set; }  
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }    
    }
}
