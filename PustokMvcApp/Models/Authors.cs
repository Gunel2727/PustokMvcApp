using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class Authors:BaseEntity
    {
        public string FullName { get; set; } = null!;
        public List<Book> Books { get; set; }  
    }
}
