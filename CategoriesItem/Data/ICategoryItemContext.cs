using Microsoft.EntityFrameworkCore;
using QuickFix.CategoriesItem.Models;

namespace QuickFix.CategoriesItem.Data
{
    public interface ICategoryItemContext
    {
        DbSet<CategoryItems> categoryItem { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
