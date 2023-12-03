using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderServ
    {
        Task<Orders?> CreateOrderAsync(string Basket_iD, string buyerEmail, int deleivryMethod, Address Add);
        Task<IReadOnlyList<Order>> OrderAsync(string buyerEmail);
        Task<Orders> getOrderById(int id , string buyerEmail);
    }
}
