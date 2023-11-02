using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.ISpacifications;
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
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetWithSpec(ISpacifications<T> Spec)
        {
            return await ApplySpac(Spec).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpesAsync(ISpacifications<T> Spec)
        {
            return await ApplySpac(Spec).ToListAsync();
        }

        private IQueryable<T> ApplySpac(ISpacifications<T> Spec)
        {
            return  SpacificationEntity<T>.Query(_Context.Set<T>(), Spec);
        }
    }
}
