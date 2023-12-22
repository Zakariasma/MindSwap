using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MindSwipe.Models
{
    public class CardUploadModel
    {
        [Required]
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? FrontImg { get; set; }
        [MaxLength(255)]
        public string? FrontImgName { get; set; }
        [NotMapped]
        public IFormFile? BackImg { get; set; }
        [MaxLength(255)]
        public string? BackImgName { get; set;}
        public int? CardID { get; set; }
    }
}
