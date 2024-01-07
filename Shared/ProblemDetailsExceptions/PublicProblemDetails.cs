using  QuickFix.Shared.Exceptions.Types;
using  QuickFix.Shared.Module;
using Microsoft.AspNetCore.Mvc;

namespace  QuickFix.Shared.ProblemDetailsExceptions
{
    public class PublicProblemDetails : ProblemDetails
    {
        public DataRespons Data { get; set; }
        public PublicProblemDetails(CustomException ex)
        {
            Title = ex.GetType().Name;
            Status = (int)ex.StatusCode;
            Data = ex.Detail;
            Detail = ex.Message;
            Type = "https://somedomain/application-rule-validation-error";
        }
    }
}
