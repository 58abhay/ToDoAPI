using Microsoft.EntityFrameworkCore;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add index to UserEmail for faster lookups
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserEmail)
                .HasDatabaseName("IX_User_UserEmail");

            // Add index to Task for better filtering
            modelBuilder.Entity<ToDo>()
                .HasIndex(t => t.Task)
                .HasDatabaseName("IX_ToDo_Task");
        }
    }
}