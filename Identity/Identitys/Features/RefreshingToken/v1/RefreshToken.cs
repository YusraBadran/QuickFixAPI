using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Identitys.Exceptions;
using QuickFix.Identity.Identitys.Features.GeneratingJwtToken.v1;
using QuickFix.Identity.Identitys.Features.GeneratingRefreshToken.v1;
using QuickFix.Identity.Identitys.Models;
using QuickFix.Identity.Shared.Models;
using QuickFix.Security.Jwt;

namespace QuickFix.Identity.Identitys.Features.RefreshingToken.v1;

public record class RefreshToken : RefreshTokenRequest, IRequest<RefreshTokenResponse>
{
    public RefreshToken(RefreshTokenRequest request) : base(request) { }
}
public class RefreshTokenValidator : AbstractValidator<RefreshToken>
{
    public RefreshTokenValidator()
    {
        RuleFor(v => v.AccessToken).NotEmpty();

        RuleFor(v => v.RefreshToken).NotEmpty();
    }
}

public class RefreshTokeHandler : IRequestHandler<RefreshToken, RefreshTokenResponse>
{
    private readonly ISender _sender;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public RefreshTokeHandler
    (
         UserManager<ApplicationUser> userManager,
         ISender sender,
         IJwtService jwtService
    )
    {
        _userManager = userManager;
        _sender = sender;
        _jwtService = jwtService;
    }
    public async Task<RefreshTokenResponse> Handle(RefreshToken request, CancellationToken cancellationToken)
    {
        var userClaimsPrincipal = _jwtService.GetPrincipalFromToken(request.AccessToken);
        if (userClaimsPrincipal is null)
        {
            throw new InvalidTokenException(userClaimsPrincipal);
        }
        var userId = userClaimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.NameId);

        var identityUser = await _userManager.FindByIdAsync(userId);

        if (identityUser == null)
            throw new IdentityUserNotFoundException(userId);

        var refreshToken = (await _sender.Send(new GenerateRefreshToken(identityUser.Id, request.RefreshToken)));

        var accessToken = await _sender.Send(
            new GenerateJwtToken(identityUser, refreshToken.Token)
        );
        return new RefreshTokenResponse(identityUser, accessToken.AccessToken, refreshToken.Token);
    }
}
