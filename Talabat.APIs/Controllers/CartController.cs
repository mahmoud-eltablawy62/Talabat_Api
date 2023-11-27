using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repostries.Contract;

namespace Talabat.APIs.Controllers
{

    public class CartController : BaseController
    {
        private readonly IBasketRepo _repo;
        private readonly IMapper _Map;
        public CartController(IBasketRepo repo  ,IMapper Map)
        {
            _repo = repo;
            _Map = Map; 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetAsync(string id)
        {
            var Basket = await _repo.GetBasketAsync(id);
            return Basket ?? new CustomerBasket(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>>UpdateAsync(CustomerBasketDto basket)
        {
            var mapper = _Map.Map<CustomerBasketDto,CustomerBasket>(basket);
            var Basket = await _repo.UpdateBasketAsync(mapper);
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
