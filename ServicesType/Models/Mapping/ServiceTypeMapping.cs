using System;
using AutoMapper;
using QuickFix.ServicesType.Models.DTOs;

namespace QuickFix.ServicesType.Models.Mapping
{
    public class ServiceTypeMapping : Profile
    {
        public ServiceTypeMapping()
        {
            CreateMap<ServiceType , ServicesTypeDTOs>();
        }

    }
}
