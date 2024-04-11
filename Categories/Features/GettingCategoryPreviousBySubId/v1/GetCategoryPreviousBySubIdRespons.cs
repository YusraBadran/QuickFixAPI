using QuickFix.Categories.Models.DTOs;

namespace QuickFix.Categories.Features.GettingCategoryPreviousBySubId.v1
{
    public record GetCategoryPreviousBySubIdRespons(IEnumerable<CategoryDTOs> Category)
    {
    }
}
