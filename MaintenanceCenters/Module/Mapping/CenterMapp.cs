using AutoMapper;
using QuickFix.MaintenanceCenters.Module.DTOs;

namespace QuickFix.MaintenanceCenters.Module.Mapping
{
    public class CenterMapp : Profile
    {
        public CenterMapp()
        {
            CreateMap<Centers, CentersDTOs>();
        }
    }
}
