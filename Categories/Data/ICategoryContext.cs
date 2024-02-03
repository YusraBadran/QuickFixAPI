using Microsoft.EntityFrameworkCore;
using QuickFix.Categories.Models;

namespace QuickFix.Categories.Data
{
    public interface ICategoryContext
    {
        DbSet<Category> category { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}

