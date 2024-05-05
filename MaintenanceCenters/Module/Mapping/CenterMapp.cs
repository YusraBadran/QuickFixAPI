using AutoMapper;
using QuickFix.MaintenanceCenters.Features.GettingCentersById.v1;
using QuickFix.MaintenanceCenters.Module.DTOs;

namespace QuickFix.MaintenanceCenters.Module.Mapping
{
    public class CenterMapp : Profile
    {
        public CenterMapp()
        {
            CreateMap<Centers, CentersDTOs>();
            CreateMap<Centers, GetCenterByIdRespons>();
            CreateMap<Centers, CentersDTOs>();
        }
    }
}
