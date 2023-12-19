using System.Net;

namespace QuickFix.Shared.Exceptions.Types;

public class NotFoundException : CustomException
{
    public NotFoundException(string message)
        : base(message)
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}
