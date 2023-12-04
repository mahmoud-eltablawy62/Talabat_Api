using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;

namespace Talabat.Core.Spacifications.Order_Spacifications
{
    public class OrderSpec : BaseSpacification<Orders>
    {
        public OrderSpec(string BuyerEmail):base(
            O => O.BuyerEmail == BuyerEmail)
        {
            Includes.Add(O => O.DelievryType);
            Includes.Add(O => O.Items);
            AddOrderByDesc(O => O.DateTime);
        }
        public OrderSpec(  int id, string BuyerEmail ) : base(
            O => O.BuyerEmail == BuyerEmail && O.Id == id)
        {
            Includes.Add(O => O.DelievryType);
            Includes.Add(O => O.Items);
           
        }


    }
}
