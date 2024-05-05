using QuickFix.Addresses.Models;
using QuickFix.Addresses.Models.DTOs;
using QuickFix.Shared.Module;

namespace QuickFix.MaintenanceCenters.Module.DTOs
{
    public class CentersDTOs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public TypeStates Status { get; set; }
        //public virtual AddressDTOs Address { get; set; }
    }
}
