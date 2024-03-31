using QuickFix.Categories.Models;
using QuickFix.Categories.Models.DTOs;

namespace QuickFix.Categories.Features.GettingCategories.v1
{
    public record GetCategoryRespons(List<CategoryDTOs> Category)
    {
    }
}
