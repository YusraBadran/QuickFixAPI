using QuickFix.ServicesType.Models;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Features.UpdateServicesType.v1
{
    public record UpdateServiceTypeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public TypeStates Status { get; set; }
    }
}
