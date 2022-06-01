using ImageServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageServer.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
