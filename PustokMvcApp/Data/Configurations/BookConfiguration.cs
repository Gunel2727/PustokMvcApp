using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PustokMvcApp.Models;

namespace PustokMvcApp.Data.Configurations
{
    public class BookConfiguration:IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
           builder.HasMany(x=>x.BookImages)
                .WithOne(x=>x.Books)
                .HasForeignKey(x=>x.BookId)
                .OnDelete(DeleteBehavior.Cascade);
           builder.Property(x=>x.Name).IsRequired().HasMaxLength(155);
        }
    }
}
