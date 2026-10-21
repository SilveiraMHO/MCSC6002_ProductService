namespace Domain
{
    public class Product
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ReOrderThreshold { get; set; }

        public Brand Brand { get; set; }
        public List<Image> Images { get; set; } = [];
    }
}
