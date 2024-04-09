using Microsoft.AspNetCore.Http;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.MaintenanceCenters.Exceptions
{
    public class CenterNameAlreadyExistException : ConflictException
    {
        public CenterNameAlreadyExistException(string name) : base($" اسم المركز {name} موجود مسبقاً")
        {
            Detail = new DataRespons
            {
                Message = $" اسم المركز {name} موجود مسبقاً",
                StatusCode = (int)HttpStatusCode.Conflict,
            };
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}

