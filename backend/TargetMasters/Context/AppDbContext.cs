using Microsoft.EntityFrameworkCore;
using TargetMasters.Models;

namespace TargetMasters.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<GuestName> GuestNames { get; set; }

        public override int SaveChanges()
        {
            ApplyDefaultValues();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyDefaultValues();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyDefaultValues()
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Id = Guid.NewGuid().ToString();
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.Deleted = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                }
            }
        }
    }
}
