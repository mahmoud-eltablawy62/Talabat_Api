using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.ISpacifications;

namespace Talabat.Repository
{
    internal static class SpacificationEntity<Tentity> where Tentity : BaseEntitiy
    {
        public static IQueryable<Tentity> Query(IQueryable<Tentity> inputQuery , ISpacifications<Tentity> spec) 
        { 
          var query = inputQuery;
            if(spec.Criteria is not  null)
            {
                query = query.Where(spec.Criteria); 
            }

            query = spec.Includes.Aggregate(query, (Current, second) => Current.Include(second));

            return query;
        }
    }
}
