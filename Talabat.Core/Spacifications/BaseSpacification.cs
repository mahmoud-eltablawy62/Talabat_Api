using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.ISpacifications;

namespace Talabat.Core.Spacifications
{
    public class BaseSpacification<T> : ISpacifications<T> where T : BaseEntitiy
    {
        public Expression<Func<T, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public BaseSpacification()
        { 
        }

        public BaseSpacification(Expression<Func<T, bool>> _Criteria)
        {
            Criteria = _Criteria;
            Includes = new List<Expression<Func<T, object>>>();
        }
    }
}
