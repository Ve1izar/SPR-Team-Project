using Microsoft.EntityFrameworkCore;

namespace LocalDriveApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Додати сюди таблиці типу користувачів, і тд
        // public DbSet<User> Users { get; set; }
        // public DbSet<FileItem> FileItems { get; set; }
    }
}