using Microsoft.EntityFrameworkCore;
using QuickFix.Categories.Data;
using QuickFix.Categories.Models;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Extensions
{
    public static class CategoryExtention
    {
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
