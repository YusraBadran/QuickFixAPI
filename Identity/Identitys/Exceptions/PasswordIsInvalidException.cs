using System;
using System.Net;
using QuickFix.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
    public class PasswordIsInvalidException : AppException
{
    public PasswordIsInvalidException(string message)
        : base(message, HttpStatusCode.Forbidden) { }
}
}
