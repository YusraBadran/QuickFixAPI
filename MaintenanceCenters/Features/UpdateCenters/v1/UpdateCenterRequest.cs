using QuickFix.Addresses.Models;
using QuickFix.Categories.Models;
using QuickFix.Shared.Module;

namespace QuickFix.MaintenanceCenters.Features.UpdateCenters.v1;

public record UpdateCenterRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TypeStates Status { get; set; }
    public Guid AddressId { get; set; }
    public virtual AddressModel Address { get; set; }
}
