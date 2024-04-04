using QuickFix.Addresses.Features.CreateAddresses.v1;
using QuickFix.Addresses.Models;

namespace QuickFix.Addresses.Features.CreateCenters.v1
{
    public record CreateCentersRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual CreateAddresRequest Address { get; set; }
    }
}
