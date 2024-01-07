using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class SuccessUpdateUserException : SuccessException
    {
        public SuccessUpdateUserException(Guid Id) : base(" تم تحديث المستخدم بنجاح")
        {
            Detail = new DataRespons
            {
                Id = Id,
                Message = " تم تحديث المستخدم بنجاح",
                StatusCode = (int)HttpStatusCode.OK
            };
            StatusCode = HttpStatusCode.OK;
        }
    }
}
