using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;

namespace Talabat.Core.Services.Contract
{
    public interface IPayementServices
    {
        Task <CustomerBasket?>  CreateOrUpdatePayement(string Basket_Id);
        Task<Orders> updateFailedOrSuccessed(string paymentId, bool succ);
    }
}
