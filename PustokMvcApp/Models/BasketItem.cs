using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class BasketItem:BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Count { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
