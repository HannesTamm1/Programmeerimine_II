using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KooliProjekt.Data
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [NotMapped]
        public string? Title { get; internal set; }
    }
}
