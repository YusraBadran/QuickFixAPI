using System;
using Microsoft.EntityFrameworkCore;
using QuickFix.ServicesType.Models;

namespace QuickFix.ServicesType.Data
{
    public interface IServiceTypeContext
    {
        DbSet<ServiceType> ServiceTypes { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }
}
