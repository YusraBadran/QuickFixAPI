
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.MaintenanceCenters.Module.DTOs;

namespace QuickFix.MaintenanceCenters.Features.GettingCenters.v1
{
    public record GetCenterRespons(IEnumerable<CentersDTOs> Category)
    {
    }
}
