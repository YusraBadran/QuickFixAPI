using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Ardalis.GuardClauses;
using QuickFix.Shared.Core.web;
using QuickFix.Shared.Exceptions.Types;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuickFix.Security.Jwt;

namespace QuickFix.Security.Extensions;

public static class Extensions
{
    public static AuthenticationBuilder AddCustomJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<JwtOptions>? optionConfigurator = null
    )
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

        services.AddJwtServices(configuration, optionConfigurator);

        var jwtOptions = configuration.BindOptions<JwtOptions>(nameof(JwtOptions));
        Guard.Against.Null(jwtOptions, nameof(jwtOptions));

        return services
            .AddAuthentication() // no default scheme specified
            .AddJwtBearer(options =>
            {
                //-- JwtBearerDefaults.AuthenticationScheme --
                options.Audience = jwtOptions.Audience;
                options.SaveToken = true;
                options.RefreshOnIssuerKeyNotFound = false;
                options.RequireHttpsMetadata = false;
                options.IncludeErrorDetails = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    SaveSigninToken = true,
                    ClockSkew = TimeSpan.Zero,
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {

                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            throw new UnAuthorizedException(" انتهت فترة الجلسة الخاصة بك. الرجاء تسجيل الدخول مرة أخرى.");
                        }
                        throw new UnAuthorizedException(
                            context.Exception.Message
                                                );
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        if (!context.Response.HasStarted)
                        {
                            throw new IdentityException(
                                "انت غير مخول للوصول ",
                                statusCode: HttpStatusCode.Unauthorized);
                        }

                        return Task.CompletedTask;
                    },
                    OnForbidden = _ => throw new ForbiddenException(" غير مصرح لك بالوصول إلى هذا المصدر "),
                };
            });
    }

    public static IServiceCollection AddJwtServices(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<JwtOptions>? optionConfigurator = null
    )
    {
        var jwtOptions = configuration.BindOptions<JwtOptions>(nameof(JwtOptions));
        Guard.Against.Null(jwtOptions, nameof(jwtOptions));

        optionConfigurator?.Invoke(jwtOptions);

        if (optionConfigurator is { })
        {
            services.Configure(nameof(JwtOptions), optionConfigurator);
        }
        else
        {
            services
                .AddOptions<JwtOptions>()
                .Bind(configuration.GetSection(nameof(JwtOptions)))
                .ValidateDataAnnotations();
        }

        services.AddTransient<IJwtService, JwtService>();

        return services;
    }

    public static IServiceCollection AddCustomAuthorization(
        this IServiceCollection services,
        IList<ClaimPolicy>? claimPolicies = null,
        IList<RolePolicy>? rolePolicies = null
    )
    {
        services.AddAuthorization(authorizationOptions =>
        {
            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme
            // https://andrewlock.net/setting-global-authorization-policies-using-the-defaultpolicy-and-the-fallbackpolicy-in-aspnet-core-3/
            var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme
            );
            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            authorizationOptions.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims
            if (claimPolicies is { })
            {
                foreach (var policy in claimPolicies)
                {
                    authorizationOptions.AddPolicy(
                        policy.Name,
                        x =>
                        {
                            x.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                            foreach (var policyClaim in policy.Claims)
                            {
                                x.RequireClaim(policyClaim.Type, policyClaim.Value);
                            }
                        }
                    );
                }
            }

            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization
            if (rolePolicies is { })
            {
                foreach (var rolePolicy in rolePolicies)
                {
                    authorizationOptions.AddPolicy(
                        rolePolicy.Name,
                        x =>
                        {
                            x.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                            x.RequireRole(rolePolicy.Roles);
                        }
                    );
                }
            }
        });

        return services;
    }

    public static void AddExternalLogins(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.BindOptions<JwtOptions>(nameof(JwtOptions));
        Guard.Against.Null(jwtOptions, nameof(jwtOptions));

        /*    if (jwtOptions.GoogleLoginConfigs is { })
            {
                services
                    .AddAuthentication()
                    .AddGoogle(googleOptions =>
                    {
                        googleOptions.ClientId = jwtOptions.GoogleLoginConfigs.ClientId;
                        googleOptions.ClientSecret = jwtOptions.GoogleLoginConfigs.ClientId;
                        googleOptions.SaveTokens = true;
                    });
            }*/
    }
}
