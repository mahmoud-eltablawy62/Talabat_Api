using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repostries.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : BaseEntitiy
    {
        private readonly StoreContext _Context;
        
        public GenaricRepo(StoreContext context) { 
              _Context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _Context.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            }
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }
    }
}
