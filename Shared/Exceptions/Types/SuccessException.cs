using  QuickFix.Shared.Module;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace  QuickFix.Shared.Exceptions.Types
{
    public class SuccessException : CustomException
    {
          public SuccessException(
           Guid Id) : base(" تمت العملية بنجاح")
        {
            Detail = new DataRespons
            {
                Id = Id,
                Message = " تمت العملية بنجاح",
                StatusCode = (int)HttpStatusCode.OK
            };
            StatusCode = HttpStatusCode.OK;
        }
    }
}
