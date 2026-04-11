using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class Author:BaseEntity
    {
        public string FullName { get; set; } = null!;
        public List<Book> Books { get; set; }  
    }
}
