using Microsoft.EntityFrameworkCore;
using QuickFix.Addresses.Models;

namespace QuickFix.Addresses.Data;

public interface IAddressDbContext
{
    public DbSet<AddressModel> address { get; set; }
    DbSet<TEntity> Set<TEntity>()
where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
