
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemByPage.v1
{
    public record GetCategoryItemByPageResponse(ListResultModel<CategoryItemDTO> CategoryItem);
}
