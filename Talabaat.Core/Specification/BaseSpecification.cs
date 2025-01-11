using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabaat.Core.Entity;

namespace Talabaat.Core.Specification
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseEntity
    {

        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, bool>> Caritria { get; set; }

        public BaseSpecification()
        {
            Includes = new List<Expression<Func<T, object>>>();

            Caritria = null;
        }
        public BaseSpecification( Expression<Func<T, bool>> cariteria)
        {
            
            this.Caritria = cariteria;
        }

      
    }
}
