using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabaat.Core.Entity
{
    public class Product:BaseEntity
    {
    
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }    //FK
        //one product in have one Brand
        public ProductBrand Brand { get; set; }
        public int CatogryId {get; set; }//FK
        public ProductCatogry Catogry { get; set; }
    }
}
