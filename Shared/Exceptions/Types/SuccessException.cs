using Microsoft.AspNetCore.Http;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.Shared.Exceptions.Types
{
  public class SuccessException : CustomException
    {
        public SuccessException() : base() { }
        public SuccessException(
                    string message,
                    HttpStatusCode statusCode = HttpStatusCode.OK,
                    params string[] errors
        )
            : base(message, statusCode, errors)
        {
            ErrorMessages = errors;
            StatusCode = HttpStatusCode.OK;

        }
        public SuccessException(
            string message,
            SuccessRequest detail,
            params string[] errors
            ):base(message)
        {
            ErrorMessages = errors;
            StatusCode = HttpStatusCode.OK;
            Detail = detail;
        }
    }
}
