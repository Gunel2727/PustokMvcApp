using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class Authors:BaseEntity
    {
        public string FullName { get; set; } = null!;
        public List<Books> Books { get; set; }  
    }
}
