using Microsoft.EntityFrameworkCore;
using QuickFix.Shared.Images.Models;

namespace QuickFix.Shared.Images.Data;

public interface IImagContext
{
    DbSet<Image> categoryItem { get; set; }
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
