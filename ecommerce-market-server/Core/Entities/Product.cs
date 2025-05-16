namespace Core.Entities
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int BrandId { get; set; }
    }
}
