using System.ComponentModel.DataAnnotations;

namespace MTG_Tavern_MVP.Models
{
    public class Card
    {
        [Key]
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string TypeLine { get; set; }
        public Uri Image {  get; set; }
    }
}
