using System;
using System.Net;
using QuickFix.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
    public class RequiresTwoFactorException : AppException
{
    public RequiresTwoFactorException(string message)
        : base(message, HttpStatusCode.BadRequest) { }
}
    
}
