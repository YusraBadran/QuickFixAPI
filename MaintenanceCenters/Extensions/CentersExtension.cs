using QuickFix.MaintenanceCenters.Data;
using QuickFix.MaintenanceCenters.Module;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.MaintenanceCenters.Extensions
{
    public static class CentersExtension
    {
        public static async Task<DataRespons> CreateAsync(
            this ICentersDbContext context,
            Centers centers)
        {
            context.centers.Add(centers);
            try
            {
                await context.SaveChangesAsync();
                return new DataRespons
                {
                    Id = centers.Id,
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
