using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabaat.Core.Entity;
using Talabaat.Core.Specification;

namespace Talabaat.Core.Reposatory.Interfaces
{
    public interface IGenericReository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
         Task<IEnumerable<T>> GetAllWithSpectAsync(ISpecifications<T> Spec);
        Task<T?> GetWithSpectAsync(ISpecifications<T> Spec);


    }
}
