using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MTG_Tavern_MVP.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Card> Cards { get; set; } = new List<Card>();

        public string Salt { get; set; }
    }
}
