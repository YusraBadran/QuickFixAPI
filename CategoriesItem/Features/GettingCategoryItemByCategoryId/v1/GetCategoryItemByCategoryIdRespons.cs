using QuickFix.Categories.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemByCategoryId.v1
{
    public record GetCategoryItemByCategoryIdRespons(List<CategoryItemDTOs> CategoryItem)
    {
    }
}
