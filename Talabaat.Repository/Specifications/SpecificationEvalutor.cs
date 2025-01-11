using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabaat.Core.Entity;
using Talabaat.Core.Specification;

namespace Talabaat.Repository.Specifications
{
    public class SpecificationEvalutor <T> where T : BaseEntity
    {
        //have method to  bulid quary
        public static IQueryable<T> GetQuary(IQueryable<T> InputQuery,ISpecifications<T> Spect)
        {
            var query=InputQuery;
            if(Spect.Caritria is not null)
            {
                query=query.Where(Spect.Caritria);

            }
           query= Spect.Includes.Aggregate(query, (CurrentQuery, IncludeExperssion) => CurrentQuery.Include(IncludeExperssion));
            //I think it is correct 
            //if (Spect.Includes.Count>0 )
            //{
            //    foreach (var item in Spect.Includes)
            //    {
            //        query=query.Include(item);
            //    }
              

            //}
            return query;

        }
    }
}
