using StackExchange.Redis;
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
using Talabat.Repository;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.Service
{
    public class ServiceOrder : IOrderServ
    {
        private readonly IBasketRepo Repo;
        private readonly IUnitOfWork _Unit;
        private readonly PaymentServices _PaymentServices;  
        

        public ServiceOrder()
        {
           
        }

        public ServiceOrder(
            IBasketRepo _Repo,
            IUnitOfWork Unit,
            PaymentServices P_Serv
           
             )
        {
            Repo = _Repo;
            _Unit = Unit;   
            _PaymentServices = P_Serv;
           
        }
        //public async Task<Orders?>CreateOrderAsync(string Basket_iD,string buyerEmail,  
        //     int deleivryMethod,Address Add)
        //{
        //    var basket = await Repo.GetBasketAsync(Basket_iD); /// ===> CuStomerBasket

        //    var orderList = new List<OrderItem>(); ////// ===> create List Of BasketList

        //    if (basket?.Items?.Count() > 0)   //// ====> Basket is empty or not 
        //    {
        //        foreach(var item in basket.Items)
        //        {
        //            var product = await _Unit.Repo<Product>().GetAsync(item.Id); ///// =====>product , i need
        //            var Prod_item = new ProductItemOrder(item.Id, product.Name, product.PictureUrl);
        //            var Order = new OrderItem(Prod_item, product.Price, item.Quantity);
        //            orderList.Add(Order);
        //        }
        //    }
        //    var Sub_Total = orderList.Sum(p => p.Quantity * p.Price);

        //    var DelievryMethod = await _Unit.Repo<DelievryType>().GetAsync(deleivryMethod);

        //    var orderSpec = new OrderSpecWithPaymentId(basket.Payment_Id);

        //    var existingOrder = await _Unit.Repo<Orders>().GetWithSpec(orderSpec);

        //    if (existingOrder != null)
        //    {
        //        _Unit.Repo<Orders>().Delete(existingOrder);
        //        await _PaymentServices.CreateOrUpdatePayement(Basket_iD);  
        //    }

        //    var order = new Orders(buyerEmail , Add , DelievryMethod , orderList, Sub_Total ,basket.Payment_Id);



        //    await _Unit.Repo<Orders>().Add(order);

        //     var result =  await _Unit.CompleteAsync();

        //    if (result <= 0)
        //    {
        //        return null;
        //    }
        //    else { return order; }

        //}


        public async Task<Orders?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shappingAddress)
        {
            // 1. Get basket from baskets Repo 

            var basket = await Repo.GetBasketAsync(basketId);

            // 2. Get selected Items at basket from product Repo 
            var orderItems = new List<OrderItem>();

            if (basket?.Items?.Count() > 0)
            {
                var productRepository = _Unit.Repo<Product>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepository.GetAsync(item.Id);

                    var productItemOrdered = new ProductItemOrder(item.Id, product.Name, product.PictureUrl);

                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                    orderItems.Add(orderItem);
                }
            }

            // 3. Calculate Subtotal 
            var subtotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            // 4. Get deliveryMethod from deliveryMethod Repo 
            var deliveryMethod = await _Unit.Repo<DelievryType>().GetAsync(deliveryMethodId);

            var orderRepo = _Unit.Repo<Orders>();

            var orderspecs = new OrderSpecWithPaymentId(basket.Payment_Id);

  
            var existingOrder = await orderRepo.GetWithSpec(orderspecs);

            if (existingOrder != null)
            {
                orderRepo.Delete(existingOrder);
                await _PaymentServices.CreateOrUpdatePayement(basketId);
            }
            // 5. Create Order 

            var order = new Orders(buyerEmail, shappingAddress, deliveryMethod, orderItems, subtotal, basket.Payment_Id);
            await orderRepo.Add(order);

            // 6. Save To Database [TODO] 
            var result = await _Unit.CompleteAsync();
            if (result <= 0) return null;

            return order;
           
        }

        public async Task<IReadOnlyList<DelievryType>> GetDelievryType() =>
                   await _Unit.Repo<DelievryType>().GetAllAsync();



        public async Task<Orders?> getOrderById(int id, string buyerEmail)
        {
            var order_repo = _Unit.Repo<Orders>();
            var spec = new OrderSpec(id , buyerEmail);
            var orders = await order_repo.GetWithSpec(spec);
            return orders;
        }

        public async Task<IReadOnlyList<Orders>> OrderAsync(string buyerEmail)
        {
           var Order_repo = _Unit.Repo<Orders>();  
            var spec  = new OrderSpec(buyerEmail);
            var orders = await Order_repo.GetAllWithSpesAsync(spec);
            return orders;

        }

     
    }
}
