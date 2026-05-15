using LocalDriveApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalDriveApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FileItem> FileItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування ієрархії папок для FileItem
            modelBuilder.Entity<FileItem>()
                .HasOne(f => f.Parent)
                .WithMany(f => f.Children)
                .HasForeignKey(f => f.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Налаштування зв'язку з User
            modelBuilder.Entity<FileItem>()
                .HasOne(f => f.User)
                .WithMany(u => u.FileItems)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade); // З юзером видаляються всі його файли
        }
    }
}