using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PustokMvcApp.Models;

namespace PustokMvcApp.Data.Configurations
{
    public class BookTagsConfiguration : IEntityTypeConfiguration<BookTags>
    {
        public void Configure(EntityTypeBuilder<BookTags> builder)
        {
            builder.HasKey(bt=>new { bt.BookId, bt.TagId });
        }
    }
}
