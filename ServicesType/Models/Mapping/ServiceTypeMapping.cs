using System;
using AutoMapper;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Features.LookUpsServiceType.v1;
using QuickFix.ServicesType.Models.DTOs;

namespace QuickFix.ServicesType.Models.Mapping
{
    public class ServiceTypeMapping : Profile
    {
        public ServiceTypeMapping()
        {
            CreateMap<ServiceType, ServicesTypeDTOs>();
            CreateMap<ServiceType, LookUpServiceTypeResponse>();
            CreateMap<ServiceType, LookUpCategoryRespons>();
        }

    }
}
