

using AutoMapper;
using QuickFix.DbContexts;
using QuickFix.Identity.Identitys.Exceptions;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Exceptions;
using QuickFix.Security.Jwt;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using IdentityConstants = QuickFix.Identity.Shared.Models.IdentityConstants;

namespace QuickFix.Identity.Identitys.Features.ResettingPassword.v1;

public record ResetPassword : ResetPasswordRequest, ICommand<DataRespons>
{
    public ResetPassword(ResetPasswordRequest request) : base(request)
    {

    }
}
public class Validator : AbstractValidator<ResetPassword>
{
    public Validator()
    {
        RuleFor(x => x.UserNameOrEmail).NotEmpty().WithMessage(" اسم المستخدم او البريد الالكتروني لا يمكن ان يكون فارغا");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage(" كلمة المرور الجديدة لا يمكن ان تكون فارغة");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(" تأكيد كلمة المرور لا يمكن ان يكون فارغا");
        RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword).WithMessage(" كلمة المرور الجديدة وتأكيد كلمة المرور غير متطابقة");
    }
}
public class RestPasswordHandler : ICommandHandler<ResetPassword, DataRespons>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ISecurityContextAccessor _security;

    private readonly ILogger<RestPasswordHandler> _logger;


    public RestPasswordHandler(UserManager<ApplicationUser> userManager, ILogger<RestPasswordHandler> logger, ISecurityContextAccessor security)
    {
        _userManager = userManager;

        _logger = logger;
        _security = security;
    }

    public async Task<DataRespons> Handle(ResetPassword request, CancellationToken cancellationToken)
    {
        var user =
            (await _userManager.FindByNameAsync(request.UserNameOrEmail)
            ?? await _userManager.FindByEmailAsync(request.UserNameOrEmail))
            ?? throw new IdentityUserNotFoundException(request.UserNameOrEmail);
        var isUser = await _userManager.IsInRoleAsync(user, IdentityConstants.Role.Admin);
        if (isUser)
        {
            throw new IdentityUserNotFoundException(request.UserNameOrEmail);
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
        if (!result.Succeeded)
        {
            throw new RegisterIdentityUserException(string.Join(',', result.Errors.Select(e => e.Description)));
        }
        throw new SuccessException(user.Id);
    }
}