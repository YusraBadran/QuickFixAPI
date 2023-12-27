using System;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Shared.ProblemDetailsExceptions
{
        public class SuccessProblemDetails : ProblemDetails
    {
        public SuccessRequest Data { get; set; }
        public SuccessProblemDetails(SuccessException ex)
        {
            Title = ex.GetType().Name;
            Status = StatusCodes.Status200OK;
            Data = ex.Detail;
            Detail = ex.Message;
            Type = "https://somedomain/application-rule-validation-error";
        }
    }
}
