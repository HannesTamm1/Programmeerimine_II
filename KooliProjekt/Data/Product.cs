using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Product : Entity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
