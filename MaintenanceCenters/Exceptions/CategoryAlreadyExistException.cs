using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.MaintenanceCenters.Exceptions
{
    public class CenterAlreadyExistException : ConflictException
    {
        public CenterAlreadyExistException() : base(" المركز موجود بالفعل")
        {
            Detail = new DataRespons
            {
                Message = $" المركز موجود بالفعل",
                StatusCode = (int)HttpStatusCode.Conflict,
            };
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
