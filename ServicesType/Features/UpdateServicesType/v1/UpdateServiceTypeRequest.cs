using QuickFix.ServicesType.Models;
using QuickFix.ServicesType.Models.DTOs;

namespace QuickFix.ServicesType.Features.UpdateServicesType.v1
{
    public record UpdateServiceTypeRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public ServicesTypeState Status { get; set; }
    }
}
