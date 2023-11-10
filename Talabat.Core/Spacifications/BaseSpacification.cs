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

        public Expression<Func<T, object>> OrederBy { get; set; } = null;

        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPagination { get ; set ; }

        public BaseSpacification()
        { 
        }

        public BaseSpacification(Expression<Func<T, bool>> _Criteria)
        {
            Criteria = _Criteria;
            Includes = new List<Expression<Func<T, object>>>();
        }

        public void AddOrderBy (Expression<Func<T, object>> OrederByExp)
        {
            OrederBy = OrederByExp;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> OrederByDescExp)
        {
            OrderByDesc = OrederByDescExp;
        }

        public void Pagination(int skip  , int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }
    }
}
