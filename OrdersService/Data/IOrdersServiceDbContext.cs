using Microsoft.EntityFrameworkCore;
using QuickFix.OrdersService.Models;

namespace QuickFix.OrdersService.Data
{
    public interface IOrdersServiceDbContext
    {
        DbSet<Orders> orders { get; set; }
        DbSet<OrderDetails> orderDetials { get; set; }
        DbSet<TEntity> Set<TEntity>()
where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
