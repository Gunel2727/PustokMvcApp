using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PustokMvcApp.Models;

namespace PustokMvcApp.Data.Configurations
{
    public class BookConfiguration:IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
           builder.HasMany(x=>x.BookImage)
                .WithOne(x=>x.Book)
                .HasForeignKey(x=>x.BookId)
                .OnDelete(DeleteBehavior.Cascade);
           builder.Property(x=>x.Name).IsRequired().HasMaxLength(155);
           builder.Property(x=>x.Price).HasPrecision(18,2);
           builder.Property(x=>x.DiscountPercent).HasPrecision(5,2);
        }
    }
}
