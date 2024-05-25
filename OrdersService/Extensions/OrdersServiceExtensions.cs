using AutoMapper;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Persistence.EfCore;
using QuickFix.Shared.Core.Queries;
using QuickFix.Shared.Module;
using Microsoft.EntityFrameworkCore;
using QuickFix.OrdersService.Data;
using QuickFix.OrdersService.Models;

namespace QuickFix.OrdersService.Extensions
{
    public static class OrdersServiceExtensions
    {
        public async static Task<IEnumerable<Orders>> NotificationsUserOrder(this IOrdersServiceDbContext context, Guid Id)
        {
            return await context.orders
                .Include(x => x.Address)
                .Include(x => x.User)
                .Where(o => o.UserId == Id)
                .ToListAsync();
        }
        public async static Task<IEnumerable<Orders>> NotificationsOrder(this IOrdersServiceDbContext context)
        {
            return await context.orders
                .Include(x => x.Address)
                .Include(x => x.User)
                .ToListAsync();
        }
        public async static Task<Orders> FindOrdersById(this IOrdersServiceDbContext context, Guid Id)
        {
            return await context.orders
                .Include(x => x.Address)
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.CategoryItem)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async static Task<IEnumerable<Orders>> FindAllOrders(this IOrdersServiceDbContext context)
        {
            return await context.orders
                .Include(x => x.Address)
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.CategoryItem)
                .ToListAsync();
        }
        public async static Task<IEnumerable<Orders>> FindAllUserOrders(this IOrdersServiceDbContext context, Guid userId)
        {
            return await context.orders
                .Include(x => x.Address)
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.CategoryItem)
                .Where(x => x.UserId == userId).ToListAsync();
        }
        public static async Task<ListResultModel<TResult>> FindAllOrdersByPageAsync<TResult>(
 this IOrdersServiceDbContext context,
   IMapper mapper,
   IPageRequest request,
   CancellationToken cancellationToken
)
   where TResult : notnull
        {
            return await context.orders
                .Include(x => x.Address)
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.CategoryItem)
                .ApplyFilter(request.Filters)
                .ApplyIncludeList(request.Includes)
                .AsNoTracking()
                .ApplyPagingAsync<Orders, TResult>(
                    mapper.ConfigurationProvider,
                    request.Page,
                    request.PageSize,
                    cancellationToken: cancellationToken
                );
        }
        public static async Task<ListResultModel<TResult>> FindAllUserOrdersByPageAsync<TResult>(
 this IOrdersServiceDbContext context,
   Guid userId,
   IMapper mapper,
   IPageRequest request,
   CancellationToken cancellationToken
)
   where TResult : notnull
        {
            return await context.orders
                .Where(o => o.UserId == userId)
               .Include(x => x.Address)
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.CategoryItem)
                .ApplyFilter(request.Filters)
                .ApplyIncludeList(request.Includes)
                .AsNoTracking()
                .ApplyPagingAsync<Orders, TResult>(
                    mapper.ConfigurationProvider,
                    request.Page,
                    request.PageSize,

                    cancellationToken: cancellationToken
                );
        }
        public static async Task<DataRespons> CreateAsync(
     this IOrdersServiceDbContext context, Orders order, List<OrderDetails> orderDetials
 )
        {
            try
            {
                context.orders.Add(order);
                context.orderDetials.AddRange(orderDetials);
                await context.SaveChangesAsync();
                return new DataRespons
                {
                    Id = order.Id,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new DataRespons
                {
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }
        public static async Task<DataRespons> UpdateAsync(
            this IOrdersServiceDbContext context, Orders order, CancellationToken cancellationToken
            )
        {
            context.orders.Update(order);
            try
            {
                await context.SaveChangesAsync(cancellationToken);
                return new DataRespons
                {
                    Id = order.Id,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new DataRespons
                {
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }
    }
}
