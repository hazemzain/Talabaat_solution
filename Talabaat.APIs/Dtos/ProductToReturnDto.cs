using Talabaat.Core.Entity;

namespace Talabaat.APIs.Dtos
{
    public class ProductToReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }    //FK
        //one product in have one Brand
        public string Brand { get; set; }
        public int CatogryId { get; set; }//FK
        public string Catogry { get; set; }
    }
}
