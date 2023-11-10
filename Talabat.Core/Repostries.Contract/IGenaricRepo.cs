using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.ISpacifications;

namespace Talabat.Core.Repostries.Contract
{
    public  interface IGenaricRepo<T> where T : BaseEntitiy
    {
        Task <T?> GetAsync(int id);  
        Task< IReadOnlyList <T> > GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithSpesAsync(ISpacifications<T> Spec);
        Task<T?> GetWithSpec(ISpacifications<T> Spec);
        Task <int> GetCountAsync(ISpacifications<T> Spec);
    }
}
