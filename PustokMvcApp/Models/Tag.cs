using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }=null!;
        public List<BookTags> BookTags { get; set; } = new List<BookTags>();
    }
}
