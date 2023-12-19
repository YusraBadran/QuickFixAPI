using Microsoft.AspNetCore.Http;
using System.Net;

namespace QuickFix.Shared.Exceptions.Types
{
    public class SuccessException : CustomException
    {
        public SuccessException(
              string message,
            params string[] errors
            ) : base(message)
        {
            ErrorMessages = errors;
            StatusCode = HttpStatusCode.OK;
        }
    }
}
