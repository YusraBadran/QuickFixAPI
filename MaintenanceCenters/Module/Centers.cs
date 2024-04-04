using QuickFix.Addresses.Models;

namespace QuickFix.MaintenanceCenters.Module;

public class Centers
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AddressId { get; set; }
    public virtual AddressModel Address { get; set; }
}
