using System;
using Microsoft.EntityFrameworkCore;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Exceptions
{
    public static class ServiceTypeExtensions
    {
        public static async Task<IEnumerable<ServiceType>> FindAllServiceType(
            this IServiceTypeContext service
        ){
            return await service.ServiceTypes.ToListAsync();
        }

        public static async Task<ServiceType> FindServiceTypeByName(
            this IServiceTypeContext service,
            string name
        ){
            return await service.ServiceTypes.FirstOrDefaultAsync(x => x.Name == name);
        }
        
        public static async Task<DataRespons> CreateAsync(
            this IServiceTypeContext service,
            ServiceType serviceType
        ){
            try
            {
                service.ServiceTypes.Add(serviceType);
                await service.SaveChangesAsync();
                return new DataRespons
                {
                    Id = serviceType.Id,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new DataRespons
                {
                    Message = ex.Message,
                    StatusCode = 400
                };
            }
        }

    }
}
