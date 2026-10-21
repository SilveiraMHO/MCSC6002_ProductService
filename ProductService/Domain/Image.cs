namespace Domain
{
    public class Image
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }

        public Product Product { get; set; }
    }
}
