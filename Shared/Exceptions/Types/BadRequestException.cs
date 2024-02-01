using System.Net;
using QuickFix.Shared.Module;

namespace  QuickFix.Shared.Exceptions.Types;

public class BadRequestException : CustomException
{
      public BadRequestException(string message)
        : base(message)
    {
        Detail = new DataRespons
        {
            Message = message,
            StatusCode = (int)HttpStatusCode.BadRequest
        };
        StatusCode = HttpStatusCode.BadRequest;
    }
}
