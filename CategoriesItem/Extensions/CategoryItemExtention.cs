using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Models;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Persistence.EfCore;
using QuickFix.Shared.Core.Queries;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Extensions
{
    public static class CategoryItemExtention
    {
        /// <summary>
        /// Finds all category item.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<CategoryItems>> FindAllCategoryItem(
                       this ICategoryItemContext context
                       )
        {
            return await context.categoryItem.Include(c => c.Category).ToListAsync();
        }
        /// <summary>
        /// Finds the category item with page.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public static async Task<ListResultModel<TResult>> FindCategoryItemWithPageAsync<TResult>(
  this ICategoryItemContext context,
IMapper mapper,
IPageRequest request,
CancellationToken cancellationToken
)
where TResult : notnull
        {
            return await context.categoryItem
            .Include(c => c.Category)
                .ApplyIncludeList(request.Includes)
                .ApplyFilter(request.Filters)
                .AsNoTracking()
                .ApplyPagingAsync<CategoryItems, TResult>(
                    mapper.ConfigurationProvider,
                    request.Page,
                    request.PageSize,
                    cancellationToken: cancellationToken
                );
        }
        /// <summary>
        /// Finds the category item by Id.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="Id">The Id.</param>
        /// <returns></returns>
        public static async Task<CategoryItems> FindCategoryItemById(
                    this ICategoryItemContext context,
                    Guid Id)
        {
            return await context.categoryItem.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == Id);
        }
        /// <summary>
        /// Finds the name of the category by.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static async Task<CategoryItems> FindCategoryItemByName(
        this ICategoryItemContext context, string name)
        {
            return await context.categoryItem.FirstOrDefaultAsync(c => c.Name == name);
        }
        /// <summary>
        /// Updates the category item.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<DataRespons> UpdateAsync(
  this ICategoryItemContext context,
    CategoryItems category
    )
        {
            try
            {
                context.categoryItem.Update(category);
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
        /// Creates the Category item.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<DataRespons> CreateAsync(
            this ICategoryItemContext context,
            CategoryItems category
       )
        {
            try
            {
                context.categoryItem.Add(category);
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
        /// Deletes the Category item.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static async Task<DataRespons> DeleteAsync(
            this ICategoryItemContext context,
            CategoryItems category
        )
        {
            try
            {
                context.categoryItem.Remove(category);
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
