using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repostries.Contract;

namespace Talabat.APIs.Controllers
{

    public class CartController : BaseController
    {
        private readonly IBasketRepo _repo;
        public CartController(IBasketRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetAsync(string id)
        {
            var Basket = await _repo.GetBasketAsync(id);
            return Basket ?? new CustomerBasket(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>UpdateAsync(CustomerBasket basket)
        {
            var Basket = await _repo.UpdateBasketAsync(basket);
            if (basket is null) return BadRequest(new ApiResponse(400));
            return Basket;
        }
        [HttpDelete]
        public async Task<bool> DeleteAsync(string basket_id)
        {
            return await _repo.DeleteBasketAsync(basket_id);
        }
    }
}
