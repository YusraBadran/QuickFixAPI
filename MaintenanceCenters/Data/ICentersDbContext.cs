using Microsoft.EntityFrameworkCore;
using QuickFix.MaintenanceCenters.Module;

namespace QuickFix.MaintenanceCenters.Data
{
    public interface ICentersDbContext
    {
        public DbSet<Centers> centers { get; set; }
        DbSet<TEntity> Set<TEntity>()
    where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
