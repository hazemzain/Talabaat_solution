using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabaat.Core.Entity;
using Talabaat.Core.Reposatory.Interfaces;
using Talabaat.Core.Specification;
using Talabaat.Repository.Data;
using Talabaat.Repository.Specifications;

namespace Talabaat.Repository.Repository
{
    public class GenaricReposatry<T> : IGenericReository<T> where T : BaseEntity
    {
        private StoreDBContext _context;

       public GenaricReposatry(StoreDBContext context)
        {
            _context=context;

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                //include to get brand content and catogry content for all product 
                return  (IEnumerable<T>) await _context.Products.Include(p=>p.Brand).Include(p=>p.Catogry).ToListAsync();
            }
             return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                //include to get brand content and catogry for the product id
                return await _context.Products.Where(p=>p.Id==id).Include(p => p.Brand).Include(p => p.Catogry).FirstOrDefaultAsync() as T;
            }
            return await _context.Set<T>().FindAsync(id);
        }




        public async Task<IEnumerable<T>> GetAllWithSpectAsync(ISpecifications<T> Spec)
        {
           return  await  SpecificationEvalutor<T>.GetQuary(_context.Set<T>(), Spec).ToListAsync();
        }

        public async Task<T?> GetWithSpectAsync( ISpecifications<T> Spec)
        {
            return await SpecificationEvalutor<T>.GetQuary(_context.Set<T>(), Spec).FirstOrDefaultAsync();

        }
    }
}
