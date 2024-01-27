using  QuickFix.Shared.Exceptions.Types;
using  QuickFix.Shared.Module;
using  QuickFix.Shared.Validation;
using Microsoft.AspNetCore.Mvc;

namespace  QuickFix.Shared.ProblemDetailsExceptions
{
    public class ValidationProblemDetail : ProblemDetails
    {
        public IEnumerable<ValidationError> Data { get; set; }
        public ValidationProblemDetail(ValidationException ex)
        {
            Title = ex.GetType().Name;
            Status = StatusCodes.Status400BadRequest;
            Data = ex.Data;
            Detail = ex.Message;
            Type = "https://somedomain/application-rule-validation-error";
        }
    }
}
