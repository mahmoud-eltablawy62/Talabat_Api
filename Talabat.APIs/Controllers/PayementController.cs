using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class PayementController : BaseController
    {
        private readonly IPayementServices p_serv;
        private readonly ILogger<PayementController> _log;
        private const string endpointSecret = "whsec_635c34611a7fd5c20e7df50037c9d7fd0300564cbef90c7ad75172c6d82b3c64";
        public PayementController(IPayementServices pserv , ILogger<PayementController> log)
        {
            p_serv = pserv;
            _log = log;
        }

        [HttpPost ("{BasketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdate (string BasketId)
        {
            var Basket = await p_serv.CreateOrUpdatePayement(BasketId);
            return Ok (Basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
           
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);
                var paymentintent = (PaymentIntent) stripeEvent.Data.Object;
            Orders order;
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                   order = await p_serv.updateFailedOrSuccessed(paymentintent.Id, true);
                    _log.LogInformation("succ");
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                     order =  await p_serv.updateFailedOrSuccessed(paymentintent.Id, true);
                    _log.LogInformation("fail");
                }
               
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
        
        }

}

