using  QuickFix.Shared.Module;
using System.Net;

namespace  QuickFix.Shared.Exceptions.Types;

public class IdentityException : CustomException
{
    public IdentityException(
        string message,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        DataRespons detail = null,
        params string[] errors
    )
        : base(message, statusCode, detail, errors) { }
}
