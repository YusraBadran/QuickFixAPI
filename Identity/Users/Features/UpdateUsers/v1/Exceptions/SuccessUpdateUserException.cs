using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
      public class SuccessUpdateUserException : SuccessException
    {
        public SuccessUpdateUserException(SuccessRequest detail):base(detail.Message)
        {
            Detail = detail;
            StatusCode = HttpStatusCode.OK;

        }
    }
}
