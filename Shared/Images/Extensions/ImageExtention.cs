using Microsoft.EntityFrameworkCore;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Models;
using QuickFix.Shared.Images.Data;
using QuickFix.Shared.Images.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Shared.Images.Extensions;

public static class ImageExtention
{
     public static async Task<IEnumerable<Image>> FindAllImage(
        this IImagContext context
        )
    {
        return await context.image.ToListAsync();
    }
    public static async Task<IEnumerable<Image>> FindAllCategoryItemImage(
        this IImagContext context,
        Guid Id
        )
    {
        return await context.image.Where(i=>i.HadImage==Id).ToListAsync();
    }
    /// <summary>
    /// Creates the image.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="category">The category.</param>
    /// <returns></returns>
    public static async Task<DataRespons> CreateImagAsync(
        this IImagContext context,
        List<Image> image
        )
    {
        try
        {
            context.image.AddRange(image);
            await context.SaveChangesAsync();
            return new DataRespons
            {
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
    /// Updates the imag asynchronous.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="image">The image.</param>
    /// <returns></returns>
    public static async Task<DataRespons> UpdateImagAsync(
        this IImagContext context,
        List<Image> image
        )
    {
        try
        {
            context.image.UpdateRange(image);
            await context.SaveChangesAsync();
            return new DataRespons
            {
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
    public static async Task<DataRespons> DeleteImagAsync(
        this IImagContext context,
        IEnumerable<Image> image
        )
    {
        try
        {
            context.image.RemoveRange(image);
            await context.SaveChangesAsync();
            return new DataRespons
            {
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
