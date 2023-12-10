using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using Talabat.Core.Spacifications;
using Talabat.Core.Spacifications.Product_Spacifications;
using Talabat.Repository;

namespace Talabat.Service
{
    public class ProductService : IProductServ
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        public Task<int> GetCount(ProductParams Param)
        {
              var countspec = new ProductCountAfterFiltration(Param);
              var Count = _unitOfWork.Repo<Product>().GetCountAsync(countspec);
              return Count;   
        }
        public  async Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductParams Param)
        {
            var spec = new ProductWithBrandAndCategory(Param);
            var products =  await _unitOfWork.Repo<Product>().GetAllWithSpesAsync(spec);
            return products;    

        }

        public async Task<Product?> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategory(id);
            var product = await _unitOfWork.Repo<Product>().GetWithSpec(spec);

            return product;
        }


        public async Task<IReadOnlyList<ProductBrand>> GetAllBrandAsync()
        {
            var Brands = await _unitOfWork.Repo<ProductBrand>().GetAllAsync();
            return Brands;
        }

      

        public async Task<IReadOnlyList<productCategory>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.Repo<productCategory>().GetAllAsync();
            return categories;
        }

    }
}
