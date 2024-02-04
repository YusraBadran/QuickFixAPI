using QuickFix.Categories.Models.DTOs;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Categories.Features.GettingCategoryByServiceTypeIdByPage.v1
{
    public record GetCategoryByServiceTypeIdByPageResponse(ListResultModel<CategoryDTOs> category);
}
