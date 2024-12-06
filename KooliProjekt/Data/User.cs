using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class User : Entity
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
    }
}
