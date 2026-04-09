namespace PustokMvcApp.Models
{
    public class BookTags
    {
        public int BookId { get; set; }
        public Books Books { get; set; } = null!;
        public int TagId { get; set; }
        public Tags Tags { get; set; } = null!;
    }
}
