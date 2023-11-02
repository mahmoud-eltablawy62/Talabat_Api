using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Spacifications.Product_Spacifications
{
    public class ProductWithBrandAndCategory : BaseSpacification<Product>
    {
        public ProductWithBrandAndCategory():base() {
            Adds();
        }

        public ProductWithBrandAndCategory(int id) : base( B=> B.Id == id )
        {
            Adds();
        }

        private void Adds()
        {
            Includes.Add(B => B.Brand);
            Includes.Add(B => B.Category);
        }
    }
}
