using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.WebSockets;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repostries.Contract;
using Talabat.Core.Spacifications;
using Talabat.Core.Spacifications.Product_Spacifications;

namespace Talabat.APIs.Controllers
{  
    public class ProductController : BaseController
    {

        private readonly IGenaricRepo<Product> _ProdRepo;
        private readonly IGenaricRepo<ProductBrand> _BrandRepo; 
        private readonly IGenaricRepo <productCategory> _CategoryRepo;  
        private readonly IMapper _Mapper;    

        public ProductController(IGenaricRepo<Product> Prod_Repo,
            IGenaricRepo<ProductBrand> BrandRepo,
            IGenaricRepo<productCategory> CategoryRepo,
            IMapper Map ) {
            _ProdRepo = Prod_Repo;  
            _BrandRepo = BrandRepo;
            _CategoryRepo = CategoryRepo;
            _Mapper = Map;
        }
        [HttpGet]
        public async Task <ActionResult<Pagination<ProductToReturnDto>>> GetAllProduct( [FromQuery] ProductParams Param)
        {
            var spec = new ProductWithBrandAndCategory(Param);
            var products = await _ProdRepo.GetAllWithSpesAsync(spec);
            var Data = _Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countspec = new ProductCountAfterFiltration(Param);
            var Count = await _ProdRepo.GetCountAsync(countspec);
            return Ok(new Pagination<ProductToReturnDto>(Param.PageSize , Param.PageIndex ,Data , Count ));
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

        
        [HttpGet ("Brands")] 
        public async Task <ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _BrandRepo.GetAllAsync();
            return Ok(Brands);
        }

        
        [HttpGet ("Categories")]
        public async Task <ActionResult<IReadOnlyList<productCategory>>> GetAllCatigories()
        {
            var categories = await _CategoryRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
