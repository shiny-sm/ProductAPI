namespace ProductAPI.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string? productname { get; set; }
        public string? description { get; set; }

        public double? price { get; set; }
    }
}
