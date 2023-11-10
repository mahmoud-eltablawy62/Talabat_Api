using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repostries.Contract
{
     public interface IBasketRepo
    {
        Task <CustomerBasket> GetBasketAsync (string id);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket Basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
