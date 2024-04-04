

using QuickFix.MaintenanceCenters.Module;

namespace QuickFix.Addresses.Models
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string description { get; set; }
        public virtual Centers Centers { get; set; } = default!;
    }
}
