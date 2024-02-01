using System;
using Microsoft.EntityFrameworkCore;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Exceptions
{
    public static class ServiceTypeExtensions
    {
        /// <summary>
        /// Finds the type of all service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<ServiceType>> FindAllServiceType(
            this IServiceTypeContext service
        )
        {
            return await service.ServiceTypes.ToListAsync();
        }
        /// <summary>
        /// Finds the name of the service type by.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static async Task<ServiceType> FindServiceTypeByName(
            this IServiceTypeContext service,
            string name
        )
        {
            return await service.ServiceTypes.FirstOrDefaultAsync(x => x.Name == name);
        }
        /// <summary>
        /// Finds the service type by identifier.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public static async Task<ServiceType> FindServiceTypeById(
        this IServiceTypeContext service,
        Guid Id
    )
        {
            return await service.ServiceTypes.FirstOrDefaultAsync(x => x.Id == Id);
        }
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public static async Task<DataRespons> CreateAsync(
            this IServiceTypeContext service,
            ServiceType serviceType
        )
        {
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
