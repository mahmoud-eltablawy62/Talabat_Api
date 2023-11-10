using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Spacifications.Product_Spacifications
{
    public class ProductWithBrandAndCategory : BaseSpacification<Product>
    {
        public ProductWithBrandAndCategory(ProductParams param) :base(
            
            p => 
            
            (string.IsNullOrEmpty(param.SearchItem) || p.Name.ToLower().Contains(param.SearchItem) ) &&
            (!param.BrandId.HasValue || p.BrandId == param.BrandId.Value) && 
            (!param.CategoryId.HasValue || p.CategoryId == param.CategoryId.Value)
            ) {
            Adds();
            if(!string.IsNullOrEmpty(param.Sort))
            {
                switch (param.Sort)
                {
                    case "price":
                        AddOrderBy(p => p.Price); 
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy( p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);    
            };

            Pagination((param.PageIndex - 1) * param.PageSize, param.PageSize);
        }

        public ProductWithBrandAndCategory(int id) : base( B=> B.Id == id )
        {
            Adds();
        }
        // PageIndex  //PageSize
        

        private void Adds()
        {
            Includes.Add(B => B.Brand);
            Includes.Add(B => B.Category);
        }
    }
}
