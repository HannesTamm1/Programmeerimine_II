using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
        public string PhotoUrl { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
