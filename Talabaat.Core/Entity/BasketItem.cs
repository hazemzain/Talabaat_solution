namespace Talabaat.Core.Entity
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string PictureName { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get;set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
    }
}