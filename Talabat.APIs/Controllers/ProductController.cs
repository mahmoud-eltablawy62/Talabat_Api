using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repostries.Contract;
using Talabat.Core.Spacifications;
using Talabat.Core.Spacifications.Product_Spacifications;

namespace Talabat.APIs.Controllers
{  
    public class ProductController : BaseController
    {

        private readonly IGenaricRepo<Product> _ProdRepo;
        private readonly IMapper _Mapper;    

        public ProductController(IGenaricRepo<Product> Prod_Repo , IMapper Map ) {
            _ProdRepo = Prod_Repo;  
            _Mapper = Map;
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            var spec = new ProductWithBrandAndCategory();
            var products = await _ProdRepo.GetAllWithSpesAsync(spec);
            return Ok(_Mapper.Map<IEnumerable<Product> , IEnumerable<ProductToReturnDto>>(products));
        }
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet ("{id}")]
        public async Task <ActionResult<ProductToReturnDto>> GetProduct(int id )
        {
            var spec = new ProductWithBrandAndCategory(id);
            var product = await _ProdRepo.GetWithSpec(spec);

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_Mapper.Map<Product , ProductToReturnDto>(product));   
        }

       
    }
}
