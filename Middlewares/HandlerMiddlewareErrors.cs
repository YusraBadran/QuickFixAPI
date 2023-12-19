
using QuickFix.Shared.Exceptions.Types;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;


namespace QuickFix.Middlewares
{
    public class HandlerMiddlewareErrors
    {
        private RequestDelegate _next;
        private IWebHostEnvironment _env;

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
            /*   catch (ValidationException ex)
               {

                   var problemDetails = new ProblemDetail(ex);
                   var response = context.Response;
                   response.ContentType = "application/json";
                   response.StatusCode = (int)ex.StatusCode;

                   await response.WriteAsync(JsonSerializer.Serialize(problemDetails));
               }*/
            /*    catch (CustomException ex)
                {

                    var problemDetails = new ProblemDetail(ex);
                    var response = context.Response;
                    response.ContentType = "application/json";
                    response.StatusCode = (int)ex.StatusCode;

                    await response.WriteAsync(JsonSerializer.Serialize(problemDetails));
                }*/
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ProblemDetails problemDetails = GetProblemDetails(ex);
                await response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }

        }
        private ProblemDetails GetProblemDetails(Exception ex)
        {
            string traceId = Guid.NewGuid().ToString();

            if (_env.EnvironmentName == "Development")
            {
                return new ProblemDetails
                {
                    Title = ex.Message,
                    Type = "https://httpstatuses.com/500",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.ToString(),
                    Instance = traceId,
                };
            }
            else
            {
                return new ProblemDetails
                {
                    Title = "Something went wrong. Please try after some time",
                    Type = "https://httpstatuses.com/500",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = @"We apologize for inconvenience. Please let us know about the error at support@orion.com. Include traceId: {traceId} in email",
                    Instance = traceId
                };
            }
        }

    }
}
