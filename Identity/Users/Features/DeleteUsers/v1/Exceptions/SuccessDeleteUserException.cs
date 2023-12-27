using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.DeleteUsers.v1.Exceptions
{
    public class SuccessDeleteUserException : SuccessException
    {
        public SuccessDeleteUserException()
        {
        }


        public SuccessDeleteUserException(SuccessRequest delail) : base(delail.Message)
        {
            Detail = delail;
            StatusCode = HttpStatusCode.OK;
        }
    }
}
