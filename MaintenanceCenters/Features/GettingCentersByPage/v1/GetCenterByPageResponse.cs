

using QuickFix.MaintenanceCenters.Module.DTOs;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.MaintenanceCenters.Features.GettingCentersByPage.v1
{
    public record GetCenterByPageResponse(ListResultModel<CentersDTOs> center);
}
