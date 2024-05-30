using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuickFix.Categories.Data;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Models;
using QuickFix.Categories.Models.DTOs;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Persistence.EfCore;
using QuickFix.Shared.Core.Queries;
using QuickFix.Shared.Module;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuickFix.Categories.Extensions
{
    public static class CategoryExtention
    {
        /// <summary>
        /// Finds the category by id.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public static async Task<Category> FindCategoryById(
            this ICategoryContext context,
            Guid Id)
        {
            return await context.category.FirstOrDefaultAsync(c => c.Id == Id);
        }
        /// <summary>
        /// Finds the category by serviceId.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="Id">The serviceId.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Category>> FindAllCategoryByServiceTypeId(
            this ICategoryContext context,
            Guid Id)
        {
            return await context.category.Where(c => c.ServiceId == Id).Include(c => c.CategoryItems).ToListAsync();
        }
        /// <summary>
        /// Finds all category.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static async Task<List<Category>> FindAllCategory(
            this ICategoryContext context
            )
        {
            return await context.category.ToListAsync();
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
        public static async Task<ListResultModel<TResult>> FindCategoryWithPageAsync<TResult>(
            this ICategoryContext category,
            IMapper mapper,
            IPageRequest request,
            CancellationToken cancellationToken
            ) where TResult : notnull
        {
            return await category.category
                .ApplyIncludeList(request.Includes)
                .ApplyFilter(request.Filters)
                .AsNoTracking()
                .ApplyPagingAsync<Category, TResult>(
                    mapper.ConfigurationProvider,
                    request.Page,
                    request.PageSize,
                    cancellationToken: cancellationToken
                );
        }
        /// <summary>
        /// Finds all category item by subId.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="Id">The subId.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Category>> FindAllCategoryBySubId(
                 this ICategoryContext context,
                 Guid Id
                 )
        {
            return await context.category.Where(c => c.SubCategoryId == Id).Include(c => c.CategoryItems).ToListAsync();
        }
        public static async Task<ListResultModel<TResult>> FindCategoryItemBySubIdWithPageAsync<TResult>(
this ICategoryContext context,
Guid Id,
IMapper mapper,
IPageRequest request,
CancellationToken cancellationToken
)
where TResult : notnull
        {
            return await context.category
                .Where(c => c.SubCategoryId == Id)
                .ApplyIncludeList(request.Includes)
                .ApplyFilter(request.Filters)
                .AsNoTracking()
                .ApplyPagingAsync<Category, TResult>(
                    mapper.ConfigurationProvider,
                    request.Page,
                    request.PageSize,
                    cancellationToken: cancellationToken
                );
        }
        /// <summary>
        /// Finds the category by servicId with page .
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="category">The category.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="request">The request.</param>
        /// <param name="Id">The servicId.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<ListResultModel<TResult>> FindCategoryByServicTypeIdWithPageAsync<TResult>(
            this ICategoryContext category,
            IMapper mapper,
            IPageRequest request,
            Guid Id,
            CancellationToken cancellationToken
            ) where TResult : notnull
        {
            return await category.category
                .Where(c => c.ServiceId == Id)
                .ApplyIncludeList(request.Includes)
                .ApplyFilter(request.Filters)
                .AsNoTracking()
                .ApplyPagingAsync<Category, TResult>(
                    mapper.ConfigurationProvider,
                    request.Page,
                    request.PageSize,
                    cancellationToken: cancellationToken
                );
        }
        /// <summary>
        /// Finds category by name.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static async Task<Category> FindCategoryByName(
            this ICategoryContext context, string name)
        {
            return await context.category.FirstOrDefaultAsync(c => c.Name == name);
        }
        /// <summary>
        /// Updates the Category.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<DataRespons> UpdateAsync(
            this ICategoryContext context, Category category
            )
        {
            try
            {
                context.category.Update(category);
                await context.SaveChangesAsync();
                return new DataRespons
                {
                    Id = category.Id,
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
        /// Creates the Category.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<DataRespons> CreateAsync(
            this ICategoryContext context, Category category
        )
        {

            try
            {
                context.category.Add(category);
                await context.SaveChangesAsync();
                return new DataRespons
                {
                    Id = category.Id,
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
        /// Deletes the Category.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<DataRespons> DeleteAsync(
            this ICategoryContext context, Category category
        )
        {
            try
            {
                context.category.Remove(category);
                await context.SaveChangesAsync();
                return new DataRespons
                {
                    Id = category.Id,
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
