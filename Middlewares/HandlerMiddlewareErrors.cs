using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Middlewares
{
    public class HandlerMiddlewareErrors
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        public HandlerMiddlewareErrors(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex){
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

            }
        }  
        private ProblemDetails GetProblemDetails( Exception ex)
        {
            string traceId = Guid.NewGuid().ToString();
            if (_env.EnvironmentName == "Development")
            {
                return  new ProblemDetails
                {
                    Title = ex.Message,
                    Type = ex.Source,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Instance = traceId,
                    Detail = ex.ToString()
                };
            }
            else
            {
                return new ProblemDetails
                {
                    Title = "Something went wrong. Please try after some time",
                    Type = ex.Source,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = @"We apologize for inconvenience. Please let us know about the error at support@orion.com. Include traceId: {traceId} in email",
                    Instance = traceId
                };
            }
        }
    }
}