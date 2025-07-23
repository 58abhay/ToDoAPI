using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToDoAPI.Infrastructure.Persistence;

namespace ToDoAPI.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=ToDoDb;Username=postgres;Password=3571h+4U",
                x => x.MigrationsAssembly("ToDoAPI.Infrastructure")
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}