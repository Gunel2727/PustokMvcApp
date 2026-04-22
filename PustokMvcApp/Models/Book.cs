
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PustokMvcApp.Attributes;
using PustokMvcApp.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMvcApp.Models
{
    public class Book : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal DiscountPercent { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Code { get; set; } = null!;
        public bool InStock { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public string MainImageUrl { get; set; } = null!;
        public string HoverImageUrl { get; set; } = null!;
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        public List<BookTag> BookTag { get; set; } = new();
        public List<BookImage> BookImage { get; set; } = new();
        [NotMapped]
        public List<int> TagIds { get; set; } = new();
        [NotMapped]
        [FileLength(2)]
        [FileTypes("image/jpeg", "image/png", "image/jpg")]
        public List<IFormFile> Files { get; set; }= new();
        [NotMapped]
        [FileLength(2)]
        [FileTypes("image/jpeg", "image/png", "image/jpg")]
        public IFormFile? MainPhoto { get; set; }
        [NotMapped]
        [FileLength(2)]
        [FileTypes("image/jpeg", "image/png", "image/jpg")]
        public IFormFile? HoverPhoto { get; set; }
    }
}