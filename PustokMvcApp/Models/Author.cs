using PustokMvcApp.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace PustokMvcApp.Models
{
    public class Author:BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; } = null!;
        public List<Book>? Books { get; set; }  

    }
}
