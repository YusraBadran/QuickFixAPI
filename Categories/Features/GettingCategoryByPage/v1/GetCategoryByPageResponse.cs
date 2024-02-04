using QuickFix.Categories.Models.DTOs;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Categories.Features.GettingCategoryByPage.v1
{
    public record GetCategoryByPageResponse(ListResultModel<CategoryDTOs> category);
}
