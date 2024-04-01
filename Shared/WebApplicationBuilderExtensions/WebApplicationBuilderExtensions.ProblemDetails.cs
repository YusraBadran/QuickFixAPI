
using QuickFix.Shared.Exceptions.Types;

using QuickFix.Shared.Validation;
using Hellang.Middleware.ProblemDetails;
using Newtonsoft.Json;
using QuickFix.Shared.ProblemDetailsExceptions;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Shared.WebApplicationBuilderExtensions;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddCustomProblemDetails(this WebApplicationBuilder builder)
    {
        builder.Services.AddProblemDetails(x =>
        {
            x.ShouldLogUnhandledException = (httpContext, exception, problemDetails) =>
            {
                var env = httpContext.RequestServices.GetRequiredService<IHostEnvironment>();
                return env.IsDevelopment() || env.IsStaging();
            };

            // Control when an exception is included
            x.IncludeExceptionDetails = (ctx, _) =>
            {
                // Fetch services from HttpContext.RequestServices
                var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
                return env.IsDevelopment() || env.IsStaging();
            };
            x.Map<ConflictException>(
                ex =>
                    new PublicProblemDetails(ex)
            );
            x.Map<SuccessException>(
                ex =>
                      new PublicProblemDetails(ex)

            );

            // Exception will produce and returns from our FluentValidation RequestValidationBehavior
            x.Map<ValidationException>(
                ex => new ValidationProblemDetail(ex)
            /*  new ProblemDetails
              {
                  Title = ex.GetType().Name,
                  Status = StatusCodes.Status400BadRequest,
                  Detail = JsonConvert.SerializeObject(ex.ValidationResultModel.Errors),
                  Type = "https://somedomain/input-validation-rules-error"
              }*/
            );
            x.Map<BadRequestException>(
                ex => new PublicProblemDetails(ex)
            );
            x.Map<ArgumentException>(
                ex =>
                    new ProblemDetails
                    {
                        Title = ex.GetType().Name,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = JsonConvert.SerializeObject(ex.Data),
                        Type = "https://somedomain/argument-error"
                    }
            );
            x.Map<NotFoundException>(
                ex =>
                    new PublicProblemDetails(ex)
            ); x.Map<FailedException>(
                ex =>
                    new ProblemDetails
                    {
                        Title = ex.GetType().Name,
                        Status = (int)ex.StatusCode,
                        Detail = JsonConvert.SerializeObject(ex.Data),
                        Type = "https://somedomain/not-found-error"
                    }
            );
            x.Map<ApiException>(
                ex =>
                    new ProblemDetails
                    {
                        Title = ex.GetType().Name,
                        Status = (int)ex.StatusCode,
                        Detail = ex.Message,
                        Type = "https://somedomain/api-server-error"
                    }
            );
            x.Map<AppException>(
                ex =>
                    new ProblemDetails
                    {
                        Title = ex.GetType().Name,
                        Status = (int)ex.StatusCode,
                        Detail = ex.Message,
                        Type = "https://somedomain/application-error"
                    }
            );
            x.Map<ForbiddenException>(ex => new ForbiddenProblemDetails(ex.Message));
            x.Map<UnAuthorizedException>(ex => new UnauthorizedProblemDetails(ex.Message));
            x.Map<IdentityException>(ex =>
            {
                var pd = new PublicProblemDetails(ex);

                return pd;
            });

            x.Map<HttpResponseException>(ex =>
            {
                var pd = new ProblemDetails
                {
                    Status = (int?)ex.StatusCode,
                    Title = ex.GetType().Name,
                    Detail = ex.Message,
                    Type = "https://somedomain/http-error"
                };

                return pd;
            });

            x.Map<HttpRequestException>(ex =>
            {
                var pd = new ProblemDetails
                {
                    Status = (int?)ex.StatusCode,
                    Title = ex.GetType().Name,
                    Detail = ex.Message,
                    Type = "https://somedomain/http-error"
                };

                return pd;
            });

            x.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
            x.MapStatusCode = context => new StatusCodeProblemDetails(context.Response.StatusCode);
        });

        return builder;
    }
}
