using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderServ _Serv;
        private readonly IMapper _mapper;
        

        public OrdersController(IOrderServ Serv, IMapper mapper  )
        {
            _Serv = Serv;
            _mapper = mapper;
             
        }
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto order)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = _mapper.Map < AddressDto, Address>(order.Addres);
            var order_As = await _Serv.CreateOrderAsync(order.Baasket_ID, BuyerEmail, order.Deleivry_Method, address);
            if (order_As == null) {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(_mapper.Map<OrderToReturnDto>(order_As));
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderByBuyerName()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _Serv.OrderAsync(BuyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(order));
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _Serv.getOrderById(id  , BuyerEmail);
            if (order is null) 
                return NotFound( new ApiResponse(404));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

        [HttpGet("DelievryMethod")]
        public async Task<ActionResult<DelievryType>> GetDelievryMethods()
        {
            var d_methods = await _Serv.GetDelievryType();
            return Ok(d_methods);
        }
       
     }
}
