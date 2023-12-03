using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity.Oreder_Aggregate;
using Talabat.Core.Repostries.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;

        private Hashtable _Repo;

        public UnitOfWork( StoreContext _context )
        {
            context = _context;
            _Repo = new Hashtable();    
        }

        public Task<int> CompleteAsync() =>
            context.SaveChangesAsync();
       

        public ValueTask DisposeAsync() =>
            context.DisposeAsync(); 
       

        public IGenaricRepo<TEntity> Repo<TEntity>() where TEntity : BaseEntitiy
        {
            var key = typeof(TEntity).Name; ////====> اعرف اسم الانتيتي الي جاي 
            /////  ===> تشوف الريبو بتاعك في الكي المطلوب ولا لا
            if( ! _Repo.ContainsKey(key) ) {
                var repo = new GenaricRepo<TEntity>(context);
                _Repo.Add(key, repo);   
            }
            return _Repo[key] as IGenaricRepo<TEntity>; 
        }
    }
}
