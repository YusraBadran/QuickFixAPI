

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
using Microsoft.Extensions.Options;

namespace QuickFix.Identity.Users.Features.ChangingPassword.v1;

public record ChangePassword : ChangePasswordRequest, ICommand<DataRespons>
{
    public ChangePassword(ChangePasswordRequest request) : base(request)
    {

    }
}
public class Validator : AbstractValidator<ChangePassword>
{
    public Validator()
    {
        RuleFor(x => x.UserNameOrEmail).NotEmpty().WithMessage(" اسم المستخدم او البريد الالكتروني لا يمكن ان يكون فارغا");
        RuleFor(x => x.OldPassword).NotEmpty().WithMessage(" كلمة المرور القديمة لا يمكن ان تكون فارغة");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage(" كلمة المرور الجديدة لا يمكن ان تكون فارغة");
        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(" تأكيد كلمة المرور لا يمكن ان يكون فارغا");
        RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword).WithMessage(" كلمة المرور الجديدة وتأكيد كلمة المرور غير متطابقة");
    }
}
public class RestPasswordHandler : ICommandHandler<ChangePassword, DataRespons>
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly ILogger<RestPasswordHandler> _logger;


    public RestPasswordHandler(UserManager<ApplicationUser> userManager, ILogger<RestPasswordHandler> logger)
    {
        _userManager = userManager;

        _logger = logger;

    }

    public async Task<DataRespons> Handle(ChangePassword request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(request.UserNameOrEmail);
        if (user == null)
        {
            throw new IdentityUserNotFoundException(request.UserNameOrEmail);
        }

        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            throw new RegisterIdentityUserException(string.Join(',', result.Errors.Select(e => e.Description)));
        }
        throw new SuccessException(user.Id);
    }
}