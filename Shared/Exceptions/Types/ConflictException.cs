using System.Net;

namespace QuickFix.Shared.Exceptions.Types;

public class ConflictException : CustomException
{
    public ConflictException(string message)
        : base(message)
    {
        StatusCode = HttpStatusCode.Conflict;
    }
}
