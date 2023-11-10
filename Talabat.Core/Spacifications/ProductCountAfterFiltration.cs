using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Spacifications
{
     public class ProductCountAfterFiltration : BaseSpacification<Product>
    {
        public ProductCountAfterFiltration(ProductParams param) : base(
              p => (string.IsNullOrEmpty(param.SearchItem) || p.Name.ToLower().Contains(param.SearchItem)) &&
            (!param.BrandId.HasValue || p.BrandId == param.BrandId.Value) &&
            (!param.CategoryId.HasValue || p.CategoryId == param.CategoryId.Value)
            )
        {
            
        }
    }
}
