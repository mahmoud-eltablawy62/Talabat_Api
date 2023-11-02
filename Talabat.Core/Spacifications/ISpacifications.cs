using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.ISpacifications
{
    public interface ISpacifications<T> where T : BaseEntitiy
    {
        public Expression<Func<T, bool>> Criteria { get; set; } 

        public List<Expression<Func<T, object>>> Includes { get; set; }
    }
  
}
