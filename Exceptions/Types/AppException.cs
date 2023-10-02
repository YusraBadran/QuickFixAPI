using System;
using System.Net;

namespace QuickFix.Exceptions.Types
{
    public class AppException:CustomException
    {
        public AppException(string message,HttpStatusCode statusCode = HttpStatusCode.BadRequest):base(message)
        {
            StatusCode = statusCode;
        }
    }
}
