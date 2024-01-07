using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class UpdateUserNotFoundException : NotFoundException
    {
        public UpdateUserNotFoundException(Guid userId)
         : base($" العميل غير موجود بالرقم {userId}.")
        {
            Detail = new DataRespons
            {
                Message = $" العميل غير موجود بالرقم {userId}.",
                StatusCode = (int)HttpStatusCode.NotFound
            };
            StatusCode = HttpStatusCode.NotFound;
        }
    }

}
