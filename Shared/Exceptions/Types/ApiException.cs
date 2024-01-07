using System.Net;

namespace  QuickFix.Shared.Exceptions.Types;

public class ApiException : CustomException
{
    public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
