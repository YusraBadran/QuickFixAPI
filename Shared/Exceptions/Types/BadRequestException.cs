using System.Net;

namespace QuickFix.Shared.Exceptions.Types;

public class BadRequestException : CustomException
{
    public BadRequestException(string message)
        : base(message)
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}
