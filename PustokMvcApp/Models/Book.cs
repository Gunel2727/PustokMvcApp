using PustokMvcApp.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMvcApp.Models
{
    public class Book:BaseEntity
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
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public List<BookTag> BookTag { get; set; } = new();
        public List<BookImage> BookImage { get; set; } = new();
    }
}
