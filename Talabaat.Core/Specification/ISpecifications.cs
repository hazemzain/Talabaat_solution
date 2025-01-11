using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabaat.Core.Entity;

namespace Talabaat.Core.Specification
{
    
    public interface ISpecifications<T> where T : BaseEntity
    {
        List<Expression<Func<T, object>>> Includes { get; set; }
        Expression<Func<T, bool>> Caritria { get;set; }
    }
       
}
// return  (IEnumerable<T>) await _context.Products.Include(p=>p.Brand).Include(p=>p.Catogry).ToListAsync();
//  return await _context.Products.Where(p=>p.Id==id).Include(p => p.Brand).Include(p => p.Catogry).FirstOrDefaultAsync() as T;

