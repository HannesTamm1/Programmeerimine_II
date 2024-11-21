using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
