namespace SaaLItems.Models
{
    public class ItemModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
    }
}
