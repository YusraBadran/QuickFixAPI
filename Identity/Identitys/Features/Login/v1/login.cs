using System;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using QuickFix.DbContexts;
using QuickFix.Identity.Identitys.Exceptions;
using QuickFix.Identity.Identitys.Features.GeneratingJwtToken.v1;
using QuickFix.Identity.Identitys.Features.GeneratingRefreshToken.v1;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Settings.Menus.Features.GettingMenu.v1;
using QuickFix.Settings.Screens.Data;
using QuickFix.Settings.Screens.Model.DTOs;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;

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
    private readonly ICommandProcessor _sender;
    private readonly ILogger<LoginHandler> _logger;

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IScreenContext _context;
    private readonly IMapper _mapper;
    public LoginHandler(
        UserManager<ApplicationUser> userManager,
        ICommandProcessor sender,
        SignInManager<ApplicationUser> signInManager,
        ILogger<LoginHandler> logger,
               IMapper mapper,
        IScreenContext context
    )
    {
        _userManager = userManager;
        _sender = sender;
        _signInManager = signInManager;
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }


    public async Task<LoginResponse> Handle(Login request, CancellationToken cancellationToken)
    {
        var identityUser = (await _userManager.FindByEmailAsync(request.UserNameOrEmail))
        ?? (await _userManager.FindByNameAsync(request.UserNameOrEmail))
        ?? throw new IdentityUserNotFoundException(request.UserNameOrEmail);
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
        else if (!signinResult.Succeeded)
        {
            throw new PasswordIsInvalidException();
        }
        if (identityUser.UserState==QuickFix.Shared.Module.TypeStates.Inactive)
        {
                throw new BadRequestException("this user is inactive");
        }
        var refreshToken = (
            await _sender.SendAsync(new GenerateRefreshToken(identityUser.Id), cancellationToken)
        ).RefreshToken;
        var accessToken = await _sender.SendAsync(
              new GenerateJwtToken(identityUser, refreshToken.Token), cancellationToken
          );
        if (string.IsNullOrWhiteSpace(accessToken.AccessToken))
        {
            throw new AppException("Generate access token failed.");
        }

        _logger.LogInformation("User with ID: {ID} has been authenticated", identityUser.Id);
        var menu = await _sender.SendAsync(new GetMenu(identityUser.Id));
        var menuItem = menu.menu;
        var userScreen = await _context.FindUserPermissionsAsync(identityUser.Id);
        var permissions = _mapper.Map<IEnumerable<UserScreenDTO>>(userScreen);
        var data = new LoginData
        {
            Id = identityUser.Id,
            FirstName = $" {identityUser.FirstName} {identityUser.LastName}",
            AccessToken = accessToken.AccessToken,
            Username = identityUser.UserName,
            RefreshToken = refreshToken.Token,
            menu = menuItem,
            Permissions = permissions,
        };
        return new LoginResponse
        {
            StatusCode = 200,
            Message = "تمت العملية بنجاح",
            Data = data

        };
    }
}