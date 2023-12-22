using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MindSwipe.Models
{
    public class Deck
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        // Reference
        // Card
        [JsonIgnore]
        public ICollection<Card> Cards { get; set; } = new List<Card>();
        // Result
        [JsonIgnore]
        public ICollection<Result> Results { get; set; } = new List<Result>();
        // Foreign key property
        // User
        public int? UserID { get; set; }
        public Users? User { get; set; }
    }
}
