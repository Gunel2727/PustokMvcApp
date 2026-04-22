using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using PustokMvcApp.Attributes;
using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class Slider:BaseEntity
    {
        [Required]
        public string Title { get; set; }=null!;
        public string Description { get; set; }=null!;
        public string? ImageUrl { get; set; }
        public string ButtonText { get; set; }=null!;
        public string ButtonUrl { get; set; }= null!;
        [NotMapped]
        [FileLength(2)]
        [FileTypes("image/jpeg", "image/png", "image/jpg")]
        public IFormFile? File { get; set; }
    }
}
