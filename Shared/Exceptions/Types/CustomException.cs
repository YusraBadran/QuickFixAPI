using System.Net;
using QuickFix.Shared.Module;

namespace QuickFix.Shared.Exceptions.Types;

public class CustomException : Exception
{
    public CustomException() : base() { }
    public CustomException(
        string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        params string[] errors
    )
        : base(message)
    {
        StatusCode = statusCode;
        ErrorMessages = errors;
        StatusCode = statusCode;
    }
    public CustomException(
        SuccessRequest detail,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError
    )
    {
        StatusCode = statusCode;
        Detail = detail;
    }


    public IEnumerable<string> ErrorMessages { get; protected set; }

    public HttpStatusCode StatusCode { get; protected set; }
    public SuccessRequest Detail { get; protected set; }
}

