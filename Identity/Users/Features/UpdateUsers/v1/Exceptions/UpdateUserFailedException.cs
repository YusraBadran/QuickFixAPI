using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class UpdateUserFailedException : FailedException
    {
        public UpdateUserFailedException(string UserName)
            : base($" Failed to Update {UserName} User")
        {
            StatusCode = HttpStatusCode.ExpectationFailed;
        }

    }
}
