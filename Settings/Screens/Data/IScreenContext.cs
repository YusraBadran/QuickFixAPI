using Microsoft.EntityFrameworkCore;
using QuickFix.Settings.Screens.Model;

namespace QuickFix.Settings.Screens.Data
{
    public interface IScreenContext
    {
        DbSet<ScreenModel> screens { get; set; }
        DbSet<UserScreen> userscreens { get; set; }
        DbSet<TEntity> Set<TEntity>()
where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
