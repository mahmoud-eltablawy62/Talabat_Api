using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.WebSockets;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repostries.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Spacifications;
using Talabat.Core.Spacifications.Product_Spacifications;

namespace Talabat.APIs.Controllers
{  
    public class ProductController : BaseController
    {

        /// private readonly IGenaricRepo<Product> _ProdRepo;
        /// private readonly IGenaricRepo<ProductBrand> _BrandRepo; 
        /// private readonly IGenaricRepo <productCategory> _CategoryRepo;  

        private readonly IProductServ _Serv;
      private readonly IMapper _Mapper;    

        public ProductController(
            ///IGenaricRepo<Product> Prod_Repo,
            ///IGenaricRepo<ProductBrand> BrandRepo,
            ///IGenaricRepo<productCategory> CategoryRepo,
            IProductServ productServ,
            IMapper Map ) {
           /// _ProdRepo = Prod_Repo;  
            ///_BrandRepo = BrandRepo;
            ///_CategoryRepo = CategoryRepo;
            _Serv = productServ;    
            _Mapper = Map;
        }
        [Authorize]
        [HttpGet]
        public async Task <ActionResult<Pagination<ProductToReturnDto>>> GetAllProduct( [FromQuery] ProductParams Param)
        {
            var products = await _Serv.GetAllProductsAsync(Param);
            var Data = _Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var count = await _Serv.GetCount(Param);
            return Ok(new Pagination<ProductToReturnDto>(Param.PageSize , Param.PageIndex ,Data , count ));
        }

        //[ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]   
        //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet ("{id}")]
        public async Task <ActionResult<ProductToReturnDto>> GetProduct(int id )
        {
           var product = await _Serv.GetProductAsync(id);   

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_Mapper.Map<Product , ProductToReturnDto>(product));   
        }

        
        [HttpGet ("Brands")] 
        public async Task <ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _Serv.GetAllBrandAsync(); 
            return Ok(Brands);
        }

        
        [HttpGet ("Categories")]
        public async Task <ActionResult<IReadOnlyList<productCategory>>> GetAllCatigories()
        {
            var categories = await _Serv.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
