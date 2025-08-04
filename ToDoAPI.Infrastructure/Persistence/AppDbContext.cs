

using Microsoft.EntityFrameworkCore;
using ToDoAPI.Domain.Entities;

namespace ToDoAPI.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<AccountProfile> AccountProfiles { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Index for fast account lookup
            modelBuilder.Entity<AccountProfile>()
                .HasIndex(a => a.Email)
                .HasDatabaseName("IX_AccountProfile_Email");

            // Index for faster task filtering
            modelBuilder.Entity<TaskItem>()
                .HasIndex(t => t.Description)
                .HasDatabaseName("IX_TaskItem_Description");

            //  Ensure Guid fields are stored as PostgreSQL 'uuid'
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Id)
                .HasColumnType("uuid");

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.AccountId)
                .HasColumnType("uuid");
        }
    }
}