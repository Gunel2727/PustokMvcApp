using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
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
        public IFormFile? File { get; set; }
    }
}
