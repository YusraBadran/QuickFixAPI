using QuickFix.Addresses.Models.DTOs;
using QuickFix.Categories.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.MaintenanceCenters.Module.DTOs;
using QuickFix.Shared.Module;

namespace QuickFix.MaintenanceCenters.Features.GettingCentersById.v1
{
    public record GetCenterByIdRespons
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public TypeStates Status { get; set; }
        public virtual AddressDTOs Address { get; set; }
    }
}
