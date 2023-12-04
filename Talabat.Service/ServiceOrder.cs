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
        //private readonly IGenaricRepo<Product> Prod_Repo;
        //private readonly IGenaricRepo<DelievryType> D_Type;
        //private readonly IGenaricRepo<Orders> Order_Repo;

        public ServiceOrder()
        {
           
        }

        public ServiceOrder(IBasketRepo _Repo,
            IUnitOfWork Unit
             //IGenaricRepo<Product> _Prod_Repo,
             //IGenaricRepo<DelievryType> _D_Type,
             //IGenaricRepo<Orders> _order_Repo
             )
        {
            Repo = _Repo;
            _Unit = Unit;   
            //Prod_Repo = _Prod_Repo; 
            //D_Type = _D_Type;
            //Order_Repo = _order_Repo;
        }

       

        public async Task<Orders?> CreateOrderAsync(string Basket_iD, string buyerEmail, int deleivryMethod, Address Add)
        {
            var Basket = await Repo.GetBasketAsync(Basket_iD);  /// ===> CuStomerBasket

            var orderList = new List<OrderItem>(); ////// ===> create List Of BasketList

            if (Basket?.Items?.Count() > 0)   //// ====> Basket is empty or not 
            {
                foreach(var item in Basket.Items)
                {
                    var product = await _Unit.Repo<Product>().GetAsync(item.Id); ///// =====>product , i need
                    var Prod_item = new ProductItemOrder(item.Id, product.Name, product.PictureUrl);
                    var Order = new OrderItem(Prod_item, product.Price, item.Quantity);
                    orderList.Add(Order);
                }
            }
            var Sub_Total = orderList.Sum(p => p.Quantity * p.Price);

            var DelievryMethod = await _Unit.Repo<DelievryType>().GetAsync(deleivryMethod);

            var order = new Orders(buyerEmail , Add , DelievryMethod , orderList, Sub_Total);

            // buyerEmail, address, delievryType, items, sub_Collection

            await _Unit.Repo<Orders>().Add(order);

             var result =  await _Unit.CompleteAsync();

            if (result <= 0)
            {
                return null;
            }
            else { return order; }

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
