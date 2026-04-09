using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Models;

namespace PustokMvcApp.Data
{
    public class PustokAppDbContext:DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public PustokAppDbContext(DbContextOptions<PustokAppDbContext> options) : base(options)
        {
        }
    }
}
