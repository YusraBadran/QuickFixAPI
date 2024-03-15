using QuickFix.Shared.Cacheing.Behaviours;
using QuickFix.Shared.Core.Reflection;
using QuickFix.Shared.Validation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using QuickFix.ServicesType.Data;
using QuickFix.DbContexts;
using QuickFix.Categories.Data;
using QuickFix.CategoriesItem.Data;

namespace QuickFix.Shared.WebApplicationBuilderExtensions;

public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {

        builder.Services.AddCqrs(
     pipelines: new[]
     {
                typeof(RequestValidationBehavior<,>),
                typeof(StreamRequestValidationBehavior<,>),
                typeof(StreamCachingBehavior<,>),
                typeof(CachingBehavior<,>),
                typeof(InvalidateCachingBehavior<,>),
     }
 );

        builder.Services.AddScoped<IServiceTypeContext>(
            options => options.GetRequiredService<AppDbContext>());
        builder.Services.AddScoped<ICategoryContext>(
            options => options.GetRequiredService<AppDbContext>());
        builder.Services.AddScoped<ICategoryItemContext>(
            options => options.GetRequiredService<AppDbContext>());

        builder.AddCustomProblemDetails();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddCustomValidators(Assembly.GetExecutingAssembly());

        return builder;
    }

}
