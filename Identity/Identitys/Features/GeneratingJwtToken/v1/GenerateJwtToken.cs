using System;
using System.Collections.Immutable;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Identitys.Models;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Shared.Models.Security.Jwt;

namespace QuickFix.Identity.Identitys.Features.GeneratingJwtToken.v1;

public record  GenerateJwtToken(ApplicationUser User, string RefreshToke) : IRequest<GenerateJwtTokenResponse>;

public class GenerateJwtTokenHandler : IRequestHandler<GenerateJwtToken, GenerateJwtTokenResponse>
{
    private readonly ILogger<GenerateJwtTokenHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public GenerateJwtTokenHandler(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService,
        ILogger<GenerateJwtTokenHandler> logger
    )
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<GenerateJwtTokenResponse> Handle(GenerateJwtToken request, CancellationToken cancellationToken)
    {
        var identityUser = request.User;
        var allClaims = await GetClaimsAsync(request.User.UserName);
        var fullName = $"{identityUser.FirstName} {identityUser.LastName}";
        var tokenResult =  _jwtService.GenerateJwtToken(
            identityUser.UserName,
            identityUser.Email,
            identityUser.Id.ToString(),
            identityUser.EmailConfirmed||identityUser.PhoneNumberConfirmed,
            fullName,
            request.RefreshToke,
            allClaims.UserClaims.ToImmutableList(),
            allClaims.Roles.ToImmutableList(),
            allClaims.PermissionClaims.ToImmutableList()
        );

        _logger.LogInformation("access-token generated, \n: {AccessToken}", tokenResult.AccessToken);
        return new GenerateJwtTokenResponse(tokenResult.AccessToken, tokenResult.ExpireAt);
    }

    public async Task<(IList<Claim> UserClaims , IList<string> Roles , IList<string> PermissionClaims)>GetClaimsAsync(
        string userName
    )
    {
         var appUser = await _userManager.FindByNameAsync(userName);

         var userClaims = (await _userManager.GetClaimsAsync(appUser)).Where(x => x.Type != CustomClaimTypes.Permission).ToList();

          var roles = await _userManager.GetRolesAsync(appUser);

          var permissions = (await _userManager.GetClaimsAsync(appUser))
            .Where(x => x.Type == CustomClaimTypes.Permission)
            ?.Select(x => x.Value)
            .ToList();

          return (UserClaims: userClaims, Roles: roles, PermissionClaims: permissions);
    }
}