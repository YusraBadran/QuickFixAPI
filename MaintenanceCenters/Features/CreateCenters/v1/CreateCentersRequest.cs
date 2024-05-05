using QuickFix.Addresses.Features.CreateAddresses.v1;
using QuickFix.Addresses.Models;
using QuickFix.Shared.Module;

namespace QuickFix.MaintenanceCenters.Features.CreateCenters.v1
{
    public record CreateCentersRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public TypeStates Status { get; set; }
        public virtual CreateAddresRequest Address { get; set; }
    }
}
