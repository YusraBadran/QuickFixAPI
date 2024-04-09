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
using QuickFix.Security.Extensions;
using QuickFix.Security.Jwt;
using QuickFix.Identity.Shared.Models;
using QuickFix.Shared.Images.Data;
using QuickFix.MaintenanceCenters.Data;
using QuickFix.Addresses.Data;
using QuickFix.Settings.Screens.Data;
using QuickFix.OrdersService.Data;

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
        builder.Services.AddScoped<IImagContext>(
            options => options.GetRequiredService<AppDbContext>());
        builder.Services.AddScoped<ICentersDbContext>(
            options => options.GetRequiredService<AppDbContext>());
        builder.Services.AddScoped<IAddressDbContext>(
            options => options.GetRequiredService<AppDbContext>());
        builder.Services.AddScoped<IScreenContext>(
            options => options.GetRequiredService<AppDbContext>());
        builder.Services.AddScoped<IOrdersServiceDbContext>(
            provider => provider.GetRequiredService<AppDbContext>());


        builder.AddCustomProblemDetails();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddCustomValidators(Assembly.GetExecutingAssembly());
        builder.Services.AddCustomJwtAuthentication(builder.Configuration);
        builder.Services.AddCustomAuthorization(
            rolePolicies: new List<RolePolicy>
            {
                new(IdentityConstants.Role.Admin, new List<string> { IdentityConstants.Role.Admin }),
                new(IdentityConstants.Role.User, new List<string> { IdentityConstants.Role.User }),
            }
        );

        return builder;
    }

}
