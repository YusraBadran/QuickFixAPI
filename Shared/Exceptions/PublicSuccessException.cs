using  QuickFix.Shared.Exceptions.Types;
using  QuickFix.Shared.Module;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace  QuickFix.Shared.Exceptions
{
    public class PublicSuccessException : SuccessException
    {
        public PublicSuccessException(string Message, Guid? Id) : base(Message)
        {
            Detail = new DataRespons
            {
                Id = Id,
                Message = Message,
                StatusCode = (int)HttpStatusCode.OK
            };
            StatusCode = HttpStatusCode.OK;
        }
        public PublicSuccessException(string Message) : base(Message)
        {
            Detail = new DataRespons
            {
                Message = Message,
                StatusCode = (int)HttpStatusCode.OK
            };
            StatusCode = HttpStatusCode.OK;
        }
    }
}
