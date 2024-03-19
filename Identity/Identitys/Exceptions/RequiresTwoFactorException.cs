using QuickFix.Shared.Exceptions.Types;
using System.Net;

namespace QuickFix.Identity.Identitys.Exceptions;

public class RequiresTwoFactorException : AppException
{
    public RequiresTwoFactorException(string message)
        : base(message, HttpStatusCode.BadRequest) { }
}
