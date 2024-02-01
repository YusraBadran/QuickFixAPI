using System;
using QuickFix.ServicesType.Models;

namespace QuickFix.ServicesType.Features.CreateServicesType.v1
{
    public record CreateServiceTypeRequest
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public ServicesTypeState Status { get; set; }
    }
}
