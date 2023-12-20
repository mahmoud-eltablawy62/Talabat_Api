using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.FinancialConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Core.Repostries.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Spacifications.Order_Spacifications;
using Talabat.Repository.Identity.Oreder_Aggregate;
using _Products = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentServices : IPayementServices
    {
        private readonly IUnitOfWork _UnitOfWork;    
        private readonly IConfiguration _Configuration;
        private readonly IBasketRepo _Repo;   
        public PaymentServices(IConfiguration config, IBasketRepo Repo , IUnitOfWork UnitOfWork)
        {
            _Configuration = config;   
            _Repo = Repo;    
            _UnitOfWork = UnitOfWork;

        }
        public async Task<CustomerBasket?> CreateOrUpdatePayement(string Basket_Id)
        {
            StripeConfiguration.ApiKey = _Configuration["stripsetting:SecretKey"];
            var basket =  await  _Repo.GetBasketAsync(Basket_Id);   
            if (basket == null)  return null;

            var price = 0m; 

            if (basket.DelievryMethodId.HasValue)
            {
                var d_method = await _UnitOfWork.Repo<DelievryType>().GetAsync(basket.DelievryMethodId.Value);
                
                basket.ShippingPrice = d_method.Cost;

                price = d_method.Cost;
            }

            if (basket?.Items.Count > 0 )
            {
                foreach(var item in basket.Items) {
                    var products = await _UnitOfWork.Repo<_Products>().GetAsync(item.Id);
                    if (item.Price != products.Price)
                         item.Price = products.Price;                                
                }
            }
            PaymentIntentService paymentIntentService = new PaymentIntentService() ;
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.Payment_Id))
            {
                var CreateOptions = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)price * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await paymentIntentService.CreateAsync(CreateOptions);
                basket.Payment_Id = paymentIntent.Id;
                basket.Secret_Name = paymentIntent.ClientSecret;
            }
            else
            {
                var updateOption = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)price * 100,
                };
               await paymentIntentService.UpdateAsync(basket.Id,updateOption);   
            }

              await _Repo.UpdateBasketAsync(basket);
            return basket;  
        }

        public async Task<Orders> updateFailedOrSuccessed(string paymentId, bool succ)
        {
            var spec = new OrderSpecWithPaymentId(paymentId);
            var order = await _UnitOfWork.Repo<Orders>().GetWithSpec(spec);
            if (succ)
            {
                order.Status = OrderStatus.PaymentSecced;
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed;
            }
             _UnitOfWork.Repo<Orders>().Update(order);
            await _UnitOfWork.CompleteAsync();
            return order;       

        }
    }
}
