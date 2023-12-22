using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindSwipe.Models
{
    public class Result
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public int Size { get; set; }
        [Required]
        public int CorrectAmount { get; set; }
        // Foreign key 
        // User
        public int? UserId { get; set; }
        public Users? User { get; set; }
        // Deck
        public int? DeckId { get; set; }
        public Deck? Deck { get; set; }
    }
}
