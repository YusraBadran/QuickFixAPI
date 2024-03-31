using System;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Features.CreateServicesType.v1
{
    public record CreateServiceTypeRequest
    {
        public string Name { get; set; }
        // public string NameEn { get; set; }
        public string Description { get; set; }
          public string? Logo { get; set; }
        public TypeStates Status { get; set; } = TypeStates.unActive;
    }
}
