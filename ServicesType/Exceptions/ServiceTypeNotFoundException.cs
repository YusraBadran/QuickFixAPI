using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.ServicesType.Exceptions
{
    public class ServiceTypeNotFoundException : NotFoundException
    {
        public ServiceTypeNotFoundException() : base($"لم يتم العثور على نوع الخدمة")
        {
            Detail = new DataRespons
            {
                Message = $"لم يتم العثور على نوع الخدمة",
                StatusCode = (int)HttpStatusCode.Conflict,
            };
            StatusCode = HttpStatusCode.Conflict;
        }

    }
}
