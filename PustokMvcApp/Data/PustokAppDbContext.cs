using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Models;

namespace PustokMvcApp.Data
{
    public class PustokAppDbContext:DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public PustokAppDbContext(DbContextOptions<PustokAppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PustokAppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
