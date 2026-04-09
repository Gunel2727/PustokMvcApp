using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class Books:BaseEntity
    {
        public string Name { get; set; }=null!;
        public string Description { get; set; }=null!;  
        public decimal DiscountPercent { get; set; }
        public decimal Price { get; set; }
        public string Code { get; set; }=null!;
        public bool InStock { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public string MainImageUrl { get; set; }=null!;
        public string HoverImageUrl { get; set; }=null!;
        public int AuthorId { get; set; }
        public Authors Authors { get; set; } = null!;
        public List<BookTags> BookTags { get; set; } = new();
        public List<BookImage> BookImages { get; set; } = new();
    }
}
