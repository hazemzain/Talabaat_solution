using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabaat.Core.Entity;

namespace Talabaat.Core.Specification.ProductSpecifications
{
    public class ProductAndBrindAndCatogrySpecifications : BaseSpecification<Product>
    {
        public ProductAndBrindAndCatogrySpecifications():base()
        {
            Includes.Add(p=>p.Brand);
            Includes.Add(p=>p.Catogry);

        }
        public ProductAndBrindAndCatogrySpecifications(int id ) : base(p=>p.Id==id)
        {
            Includes = new List<Expression<Func<Product, object>>>();
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Catogry);

        }
    } 
}
