using Microsoft.AspNetCore.Builder;

namespace QuickFix.Middlewares;

public static class MiddlewaresExtensions
{
    public static IApplicationBuilder UseRequestLogContextMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLogContextMiddleware>();
    }
}
