using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Identitys.Exceptions;
using QuickFix.Identity.Identitys.Features.GeneratingJwtToken.v1;
using QuickFix.Identity.Identitys.Features.GeneratingRefreshToken.v1;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Security.Jwt;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Identity.Identitys.Features.RefreshingToken.v1;

public record class RefreshToken(string AccessTokenData, string RefreshTokenData) : ICommand<RefreshTokenResponse>
{

}
public class RefreshTokenValidator : AbstractValidator<RefreshToken>
{
    public RefreshTokenValidator()
    {
        RuleFor(v => v.AccessTokenData).NotEmpty();

        RuleFor(v => v.RefreshTokenData).NotEmpty();
    }
}

public class RefreshTokeHandler : ICommandHandler<RefreshToken, RefreshTokenResponse>
{
    private readonly ICommandProcessor _sender;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public RefreshTokeHandler
    (
         UserManager<ApplicationUser> userManager,
         ICommandProcessor sender,
         IJwtService jwtService
    )
    {
        _userManager = userManager;
        _sender = sender;
        _jwtService = jwtService;
    }
    public async Task<RefreshTokenResponse> Handle(RefreshToken request, CancellationToken cancellationToken)
    {
        var userClaimsPrincipal = _jwtService.GetPrincipalFromToken(request.AccessTokenData);
        if (userClaimsPrincipal is null)
        {
            throw new InvalidTokenException(userClaimsPrincipal);
        }
        var userId = userClaimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.NameId);

        var identityUser = await _userManager.FindByIdAsync(userId);

        if (identityUser == null)
            throw new IdentityUserNotFoundException(userId);

        var refreshToken = (
                        await _sender.SendAsync(
                            new GenerateRefreshToken(identityUser.Id, request.RefreshTokenData),
                            cancellationToken
                        )
                    ).RefreshToken;
        var accessToken = await _sender.SendAsync(
      new GenerateJwtToken(identityUser, refreshToken.Token),
      cancellationToken
  );
        return new RefreshTokenResponse(identityUser, accessToken.AccessToken, refreshToken.Token);
    }
}
