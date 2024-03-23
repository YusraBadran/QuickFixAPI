using QuickFix.Shared.Module;
using System;

namespace QuickFix.ServicesType.Models.DTOs
{
    public class ServicesTypeDTOs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeStates Status { get; set; }
    }
}
