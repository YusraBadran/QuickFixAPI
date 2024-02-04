using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuickFix.Categories.Data;
using QuickFix.Categories.Models;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Persistence.EfCore;
using QuickFix.Shared.Core.Queries;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Extensions
{
    public static class CategoryExtention
    {
        public static async Task<Category> FindCategoryById(
            this ICategoryContext context,
            Guid Id)
        {
            return await context.category.FirstOrDefaultAsync(c => c.Id == Id);
        }
        public static async Task<IEnumerable<Category>> FindCategoryByServiceTypeId(
            this ICategoryContext context,
            Guid Id)
        {
            return await context.category.Where(c => c.ServiceId == Id).ToListAsync();
        }
        public static async Task<IEnumerable<Category>> FindAllCategory(
            this ICategoryContext context
            )
        {
            return await context.category.ToListAsync();
        }
        public static async Task<ListResultModel<TResult>> FindCategoryWithPageAsync<TResult>(
this ICategoryContext category,
IMapper mapper,
IPageRequest request,
CancellationToken cancellationToken
)
where TResult : notnull
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
        public static async Task<ListResultModel<TResult>> FindCategoryByServicTypeIdWithPageAsync<TResult>(
this ICategoryContext category,
IMapper mapper,
IPageRequest request,
Guid Id,
CancellationToken cancellationToken
)
where TResult : notnull
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
        /// Finds the name of the category by.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static async Task<Category> FindCategoryByName(
        this ICategoryContext context, string name)
        {
            return await context.category.FirstOrDefaultAsync(c => c.Name == name);
        }
        public static async Task<DataRespons> CreateAsync(
            this ICategoryContext context,
            Category category
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
    }
}
