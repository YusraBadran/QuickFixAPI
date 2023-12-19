using System.Net;

namespace QuickFix.Shared.Exceptions.Types;

public class ForbiddenException : IdentityException
{
    public ForbiddenException(string message)
        : base(message, statusCode: HttpStatusCode.Forbidden) { }
}
