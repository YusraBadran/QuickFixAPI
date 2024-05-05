using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Models;
using QuickFix.MaintenanceCenters.Data;
using QuickFix.MaintenanceCenters.Module;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Persistence.EfCore;
using QuickFix.Shared.Core.Queries;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.MaintenanceCenters.Extensions
{
    public static class CentersExtension
    {
        /// <summary>
        /// Finds all category item.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Centers>> FindAllCentersAsync(
                       this ICentersDbContext context
                       )
        {
            return await context.centers.ToListAsync();
        }
        /// <summary>
        /// Finds the category item by Id.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="Id">The Id.</param>
        /// <returns></returns>
        public static async Task<Centers> FindCentersById(
                    this ICentersDbContext context,
                    Guid Id)
        {
            return await context.centers.Include(a => a.Address).FirstOrDefaultAsync(c => c.Id == Id);
        }

        /// <summary>
        /// Finds the name of the category by.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static async Task<Centers> FindCenterByName(
        this ICentersDbContext context, string name)
        {
            return await context.centers.Include(a => a.Address).FirstOrDefaultAsync(c => c.Name == name);
        }
        /// <summary>
        /// Finds the category with page .
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="category">The category.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<ListResultModel<TResult>> FindCentersWithPageAsync<TResult>(
            this ICentersDbContext context,
            IMapper mapper,
            IPageRequest request,
            CancellationToken cancellationToken
            ) where TResult : notnull
        {
            return await context.centers
                .ApplyIncludeList(request.Includes)
                .ApplyFilter(request.Filters)
                .AsNoTracking()
                .ApplyPagingAsync<Centers, TResult>(
                    mapper.ConfigurationProvider,
                    request.Page,
                    request.PageSize,
                    cancellationToken: cancellationToken
                );
        }
        public static async Task<DataRespons> CreateAsync(
    this ICentersDbContext context,
    Centers centers)
        {
            context.centers.Add(centers);
            try
            {
                await context.SaveChangesAsync();
                return new DataRespons
                {
                    Id = centers.Id,
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
        /// <summary>
        /// Deletes the Category item.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<DataRespons> DeleteAsync(
            this ICentersDbContext context,
            Centers center
        )
        {
            try
            {
                context.centers.Remove(center);
                await context.SaveChangesAsync();
                return new DataRespons
                {
                    Id = center.Id,
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
        /// <summary>
        /// Updates the category item.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<DataRespons> UpdateAsync(
  this ICentersDbContext context,
    Centers center
    )
        {
            try
            {
                context.centers.Update(center);
                await context.SaveChangesAsync();
                return new DataRespons
                {
                    Id = center.Id,
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
