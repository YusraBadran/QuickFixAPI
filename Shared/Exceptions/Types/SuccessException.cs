using  QuickFix.Shared.Module;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace  QuickFix.Shared.Exceptions.Types
{
    public class SuccessException : CustomException
    {
        public SuccessException(
            string message,
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
}
