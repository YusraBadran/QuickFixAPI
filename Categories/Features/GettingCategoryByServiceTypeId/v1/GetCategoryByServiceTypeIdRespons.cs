using QuickFix.Categories.Models.DTOs;

namespace QuickFix.Categories.Features.GettingCategoryByServiceTypeId.v1
{
    public record GetCategoryByServiceTypeIdRespons(IEnumerable<CategoryDTOs> Category)
    {
    }
}
