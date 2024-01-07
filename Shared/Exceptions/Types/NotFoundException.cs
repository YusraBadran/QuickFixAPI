using  QuickFix.Shared.Module;
using System.Net;

namespace  QuickFix.Shared.Exceptions.Types;

public class NotFoundException : CustomException
{
    public NotFoundException(string message,
            DataRespons? detail = null,
          HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
          params string[] errors
          ) : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
        Detail = detail;
    }
}
