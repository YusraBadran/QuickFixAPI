using System.Net;

namespace QuickFix.Shared.Exceptions.Types;

// https://stackoverflow.com/questions/21097730/usage-of-ensuresuccessstatuscode-and-handling-of-httprequestexception-it-throws
public class HttpResponseException : CustomException
{
    public HttpResponseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message, statusCode) { }
}
