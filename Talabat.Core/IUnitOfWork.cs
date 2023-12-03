using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Core.Repostries.Contract;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable 
    {
        IGenaricRepo<TEntity> Repo<TEntity>() where TEntity : BaseEntitiy; 
        Task<int> CompleteAsync();
    }
    
      
       
    
}
