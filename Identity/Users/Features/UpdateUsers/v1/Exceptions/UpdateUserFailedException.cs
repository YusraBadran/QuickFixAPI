using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class UpdateUserFailedException : FailedException
    {
        public UpdateUserFailedException(string UserName)
            : base($" فشل تحديث بيانات المستخدم بالرقم القومي: {UserName}.")
        {
            Detail = new DataRespons
            {
                Message = $" فشل تحديث بيانات المستخدم بالرقم القومي: {UserName}.",
                StatusCode = (int)HttpStatusCode.ExpectationFailed
            };
            StatusCode = HttpStatusCode.ExpectationFailed;
        }

    }
}
