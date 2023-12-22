using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace MindSwipe.Models
{
    public class Card
    {
        [Required]
        public int Id {  get; set; }
        [MaxLength(255)]
        public string FrontText {  get; set; }
        [MaxLength(255)]
        public string BackText {  get; set; }
        [MaxLength(255)]
        public string FrontImg {  get; set; }
        [MaxLength(255)]
        public string BackImg { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        // Foreign key
        public int? DeckId { get; set; }
        public Deck? Deck { get; set; }
    }
}
