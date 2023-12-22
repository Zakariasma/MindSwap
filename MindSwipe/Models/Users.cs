using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MindSwipe.Models
{
    [Table("Users")]
    public class Users
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(24)]
        public string Username { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [MinLength(9)]
        [MaxLength(255)]
        public byte[] PasswordHash { get; set; }
        [MinLength(9)]
        [MaxLength(255)]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        // Collection containing dependents
        //Deck
        [JsonIgnore]
        public ICollection<Deck> Decks { get; } = new List<Deck>();
        //Deck
        [JsonIgnore]
        public ICollection<Result> Results { get; } = new List<Result>();
    }
}
