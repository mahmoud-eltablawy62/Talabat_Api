using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity.Oreder_Aggregate
{
    public class ProductItemOrder : BaseEntitiy
    {
        public ProductItemOrder() { }
        public ProductItemOrder(int product_Id, string product_Name, string picture_Url)
        {
            Prod_Id = product_Id;
            Prod_Name = product_Name;
            Picture_Url = picture_Url;
        }

        public int Prod_Id { get; set; } 
        public string Prod_Name { get; set;}  
        public string Picture_Url { get; set;}
    
    }
}
