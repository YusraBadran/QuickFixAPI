using QuickFix.ServicesType.Models.DTOs;

namespace QuickFix.ServicesType.Features.GettingServicesType.v1
{
    public record GetServicesTypeRespons(IEnumerable<ServicesTypeDTOs> serviceType);
}
