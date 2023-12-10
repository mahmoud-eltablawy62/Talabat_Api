using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Spacifications;

namespace Talabat.Core.Services.Contract
{
    public interface IProductServ
    {
        Task <IReadOnlyList<Product>> GetAllProductsAsync (ProductParams Param);

        Task <int> GetCount (ProductParams Param);  
        Task <Product?> GetProductAsync (int id);    
        Task <IReadOnlyList<ProductBrand>> GetAllBrandAsync ();

        Task<IReadOnlyList<productCategory>> GetCategoriesAsync();
    }
}
