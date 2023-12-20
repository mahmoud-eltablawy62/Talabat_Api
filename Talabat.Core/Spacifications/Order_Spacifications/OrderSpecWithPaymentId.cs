using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;

namespace Talabat.Core.Spacifications.Order_Spacifications
{
    public class OrderSpecWithPaymentId : BaseSpacification<Orders>
    {
        public OrderSpecWithPaymentId(string paymentId) : 
            base( o => o.PaymentId == paymentId )
        {            
        }
    }
}
