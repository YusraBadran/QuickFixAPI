
using QuickFix.CategoriesItem.Models.DTOs;

namespace QuickFix.CategoriesItem.Features.GettingCategoriesItem.v1
{
    public record GetCategoryItemRespons(IEnumerable<CategoryItemDTO> CategoryItem)
    {
    }
}
