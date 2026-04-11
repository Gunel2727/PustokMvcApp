using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PustokMvcApp.Models;

namespace PustokMvcApp.Data.Configurations
{
    public class BookTagsConfiguration : IEntityTypeConfiguration<BookTag>
    {
        public void Configure(EntityTypeBuilder<BookTag> builder)
        {
            builder.HasKey(bt=>new { bt.BookId, bt.TagId });
        }
    }
}
