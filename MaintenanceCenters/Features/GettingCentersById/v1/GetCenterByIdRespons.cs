using QuickFix.Categories.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.MaintenanceCenters.Module.DTOs;

namespace QuickFix.MaintenanceCenters.Features.GettingCentersById.v1
{
    public record GetCenterByIdRespons(CentersDTOs center)
    {
    }
}
