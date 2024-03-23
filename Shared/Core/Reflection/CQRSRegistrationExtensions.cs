using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Commands;
using QuickFix.Shared.Core.Queries;
using System.Reflection;
using MediatR;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Commands;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Shared.Core.Reflection;

public static class CQRSRegistrationExtensions
{
    public static IServiceCollection AddCqrs(
        this IServiceCollection services,
        Assembly[]? assemblies = null,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient,
        params Type[] pipelines
    )
    {
        foreach (var pipeline in pipelines)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), pipeline);
        }
        services.AddScoped<ICommandProcessor, CommandProcessor>();
        services.AddScoped<IQueryProcessor, QueryProcessor>();
        return services;
    }
}
