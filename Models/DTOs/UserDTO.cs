using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MTG_Tavern_MVP.Models.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
