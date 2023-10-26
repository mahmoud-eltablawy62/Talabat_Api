using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repostries.Contract;

namespace Talabat.APIs.Controllers
{  
    public class ProductController : BaseController
    {

        private readonly IGenaricRepo<Product> _ProdRepo;

        public ProductController(IGenaricRepo<Product> Prod_Repo ) {
            _ProdRepo = Prod_Repo;  
        }
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Product>>> GetProduct()
        {
            
            var product = await _ProdRepo.GetAllAsync();
            return Ok( product );
        }

        [HttpGet ("{id}")]
        public async Task <ActionResult<Product>> GetProduct(int id )
        {
            var product = await _ProdRepo.GetAsync( id );   

            if( product == null ) {
                return NotFound(new {message="NotFound"  , statusCode=404});
            }

            return Ok( product );   
        }  
    }
}
