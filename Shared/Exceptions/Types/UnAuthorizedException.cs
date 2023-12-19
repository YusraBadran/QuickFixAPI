using System.Net;

namespace QuickFix.Shared.Exceptions.Types;

public class UnAuthorizedException : IdentityException
{
    public UnAuthorizedException(string message)
        : base(message, HttpStatusCode.Unauthorized) { }
}
