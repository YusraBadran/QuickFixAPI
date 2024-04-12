using QuickFix.Categories.Models.DTOs;

namespace QuickFix.Categories.Features.GettingCategoryBySubId.v1
{
    public record GetCategoryBySubIdRespons(IEnumerable<CategoryDTOs> Category)
    {
    }
}
