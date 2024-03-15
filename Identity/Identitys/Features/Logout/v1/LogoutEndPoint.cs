using System;
using EasyCaching.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuickFix.Security.Jwt;
using QuickFix.Shared.Abstractions.Caching;

namespace QuickFix.Identity.Identitys.Features.Logout.v1;

public class LogoutController : Controller
{
    private readonly ILogger<LogoutController> _logger;
    private readonly CancellationToken _cancellationToken;
    private readonly IHttpContextAccessor _httpContext;
    // private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEasyCachingProviderFactory _cachingProviderFactory;
    private readonly IOptions<JwtOptions> _jwtOptions;
    public LogoutController(ILogger<LogoutController> logger, IHttpContextAccessor httpContext, IEasyCachingProviderFactory cachingProviderFactory, IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger;

        _httpContext = httpContext;
        _cachingProviderFactory = cachingProviderFactory;
        _jwtOptions = jwtOptions;
    }
    [Route("api/identity/logout/v1")]
    [ApiExplorerSettings(GroupName = "identity")]
    [HttpPost]
    public async Task<IResult> LogoutMeth()
    {
        await Logout(_httpContext.HttpContext, _cachingProviderFactory, _jwtOptions);

        return Results.Ok();

    }
    private static async Task<IResult> Logout(
    HttpContext httpContext,
    IEasyCachingProviderFactory cachingProviderFactory,
    IOptions<JwtOptions> jwtOptions
)
    {
        var cacheProvider = cachingProviderFactory.GetCachingProvider(nameof(CacheProviderType.InMemory));

        await httpContext.SignOutAsync();

        if (jwtOptions.Value.CheckRevokedAccessTokens)
        {

            var token = GetTokenFromHeader(httpContext);
            var userName = httpContext.User.Identity!.Name;
            await cacheProvider.SetAsync(
                $"{userName}_{token}_revoked_token",
                token,
                TimeSpan.FromSeconds(jwtOptions.Value.TokenLifeTimeSecond)
            );
        }

        return Results.Ok();
    }

    private static string GetTokenFromHeader(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers.Values.ToString();
        return authorizationHeader;
    }
}
