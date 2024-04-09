using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.MaintenanceCenters.Exceptions
{
    public class CenterNotFoundException : NotFoundException
    {
        public CenterNotFoundException() : base($" الفئه غير موجوده")
        {
            Detail = new DataRespons
            {
                Message = $" الفئه غير موجوده",
                StatusCode = (int)HttpStatusCode.Conflict,
            };
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
