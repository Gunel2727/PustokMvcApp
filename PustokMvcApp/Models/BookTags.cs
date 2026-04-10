namespace PustokMvcApp.Models
{
    public class BookTags
    {
        public int BookId { get; set; }
        public Book Books { get; set; } = null!;
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
