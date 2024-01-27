using  QuickFix.Shared.Module;
using System.Net;

namespace  QuickFix.Shared.Exceptions.Types;

public class CustomException : Exception
{
    public CustomException()
    {

    }
    public CustomException(
        string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        DataRespons detail = null,
        params string[] errors
    )
        : base(message)
    {
        StatusCode = statusCode;
        ErrorMessages = errors;
        Detail = detail;
    }
    public IEnumerable<string> ErrorMessages { get; protected set; }

    public HttpStatusCode StatusCode { get; protected set; }
    public DataRespons Detail { get; protected set; }
}
