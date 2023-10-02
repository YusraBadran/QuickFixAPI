using System;
using System.Net;

namespace QuickFix.Exceptions.Types
{
    public class BadRequestException:CustomException
    {
        public BadRequestException(string message):base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
