using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class BookImage:BaseEntity
    {
        public string ImageUrl { get; set; }=null!;
        public int BookId { get; set; }
        public Books Books { get; set; } = null!;
    }
}
