using System;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using QuickFix.Dbcontexts;
using QuickFix.Exceptions.Types;
using QuickFix.Identity.Identitys.Exceptions;
using QuickFix.Identity.Identitys.Features.GeneratingJwtToken.v1;
using QuickFix.Identity.Identitys.Features.GeneratingRefreshToken.v1;
using QuickFix.Identity.Identitys.Models;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Shared.Models.Security.Jwt;

namespace QuickFix.Identity.Identitys.Features.Login.v1;
public record class Login : LoginRequest, IRequest<LoginResponse>
{
    public Login(LoginRequest request) : base(request) { }
}

public class LoginValidator : AbstractValidator<Login>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserNameOrEmail).NotEmpty().WithMessage("UserNameOrEmail cannot be empty");
        RuleFor(x => x.password).NotEmpty().WithMessage("password cannot be empty");
    }
}

public class LoginHandler : IRequestHandler<Login, LoginResponse>
{
    private readonly ISender _sender;
    private readonly ILogger<LoginHandler> _logger;

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginHandler(
        UserManager<ApplicationUser> userManager,
        ISender sender,
        SignInManager<ApplicationUser> signInManager,
        ILogger<LoginHandler> logger
    )
    {
        _userManager = userManager;
        _sender = sender;
        _signInManager = signInManager;
        _logger = logger;
    }


    public async Task<LoginResponse> Handle(Login request, CancellationToken cancellationToken)
    {
        var identityUser = (await _userManager.FindByEmailAsync(request.UserNameOrEmail)) 
        ?? (await _userManager.FindByNameAsync(request.UserNameOrEmail))
        ?? throw new LoginFailedException(request.UserNameOrEmail);
        var signinResult = await _signInManager.CheckPasswordSignInAsync(identityUser, request.password, false);
        if (signinResult.IsNotAllowed)
        {
            if (await _userManager.IsEmailConfirmedAsync(identityUser))
            {
                throw new EmailNotConfirmedException(identityUser.Email);
            }
            if (await _userManager.IsPhoneNumberConfirmedAsync(identityUser))
            {
                throw new PhoneNumberNotConfirmedException(identityUser.PhoneNumber);
            }
        }
        else if (signinResult.IsLockedOut)
        {
            throw new UserLockedException(identityUser.Id.ToString());
        }
        else if (signinResult.RequiresTwoFactor)
        {
            throw new RequiresTwoFactorException("Require two factor authentication.");
        }
        else if(!signinResult.Succeeded)
        {
            throw new PasswordIsInvalidException("Password is invalid.");
        }
        var refreshToken =  await _sender.Send(new GenerateRefreshToken(identityUser.Id), cancellationToken);

        var  accessToken = await _sender.Send(new GenerateJwtToken(identityUser, refreshToken.Token), cancellationToken);

        if (string.IsNullOrWhiteSpace(accessToken.Token))
        {
            throw new AppException("Generate access token failed.");
        }

         _logger.LogInformation("User with ID: {ID} has been authenticated", identityUser.Id);

         return new LoginResponse(identityUser, accessToken.Token, refreshToken.Token);
    }
}