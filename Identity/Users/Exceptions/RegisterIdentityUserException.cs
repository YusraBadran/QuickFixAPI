using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;


namespace QuickFix.Identity.Users.Exceptions
{
    public class RegisterIdentityUserException:AppException
    {
        public RegisterIdentityUserException(string errors):base(errors, HttpStatusCode.InternalServerError)
        {
        }
    }
}
