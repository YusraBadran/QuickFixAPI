using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Exceptions;
using QuickFix.Identity.Users.Features.Register.v1;
using QuickFix.Identity.Users.Features.Register.v1.Exceptions;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Identity.Users.Models.RegisterUser;
using QuickFix.Settings.Screens.Data;
using QuickFix.Settings.Screens.Extensions;
using QuickFix.Settings.Screens.Model;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using IdentityConstants = QuickFix.Identity.Shared.Models.IdentityConstants;
using UserState = QuickFix.Identity.Shared.Models.UserState;

namespace QuickFix.Users.Features.Register.v1;




public record class RegisterUser : RegisterUserRequest, ITxCreateCommand<RegisterUserResponse>
{
    public DateTime CreatedAt { get; init; }

    public RegisterUser(RegisterUserRequest request) : base(request)
    {
        CreatedAt = DateTime.UtcNow;
    }
}
public class Validator : AbstractValidator<RegisterUser>
{

    public Validator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(v => v.FirstName).NotNull().NotEmpty().WithMessage(" الاسم الاول مطلوب ");
        RuleFor(v => v.LastName).NotNull().NotEmpty().WithMessage(" الاسم الاخير مطلوب ");

        RuleFor(v => v.Email).NotNull().NotEmpty().WithMessage(" البريد الالكتروني مطلوب ")
            .EmailAddress().WithMessage("البريد الالكتروني غير صحيح");

        RuleFor(v => v.UserName).NotNull().NotEmpty().WithMessage(" اسم المستخدم مطلوب ")
            .MinimumLength(6).WithMessage("اسم المستخدم يجب ان يكون اكثر من 6 احرف")
            .MaximumLength(20).WithMessage("اسم المستخدم يجب ان يكون اقل من 20 حرف");

        RuleFor(p => p.PhoneNumber)
            .NotNull().NotEmpty()
            .WithMessage(" رقم الهاتف مطلوب ")
            .MinimumLength(7)
            .WithMessage("رقم الهاتف يجب ان يكون اكثر من 7 ارقام")
            .MaximumLength(15)
            .WithMessage("رقم الهاتف يجب ان يكون اقل من 15 رقم");
        RuleFor(v => v.ConfirmPassword)
        .Equal(v => v.Password)
        .WithMessage(" كلمة المرور غير متطابقة ");
        RuleFor(v => v.Roles)
        .Custom(
            (roles, c) =>
            {
                if (roles != null && !roles.All(
                    x => x.Contains(IdentityConstants.Role.Admin, StringComparison.Ordinal) ||
                    x.Contains(IdentityConstants.Role.User, StringComparison.Ordinal)
                ))
                {
                    c.AddFailure(" يجب ان يكون الرول ادمن او يوزر ");
                }
            }
        );
    }
}
/**
* start handler
*/
public class RegisterHandler : ICommandHandler<RegisterUser, RegisterUserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IScreenContext _screenContext;
    private readonly ILogger<RegisterHandler> _logger;
    public RegisterHandler(UserManager<ApplicationUser> userManager, ILogger<RegisterHandler> logger, IScreenContext screenContext)
    {
        _userManager = userManager;
        _logger = logger;
        _screenContext = screenContext;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        var applicationUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserState = TypeStates.Inactive,
            CreatedAt = request.CreatedAt,
        };

        var username = await _userManager.FindByNameAsync(request.UserName);
        if (username != null)
        {
            throw new RegisterExistUserNameException("Username already exists.");
        }

        var email = await _userManager.FindByEmailAsync(request.Email);
        if (email != null)
        {
            throw new RegisterExistEmailException("Email already exists.");
        }

        var phoneNumber = await _userManager.FindByPhoneNumberAsync(request.PhoneNumber);
        if (phoneNumber != null)
        {
            throw new RegisterExistPhonNumberException("PhoneNumber already exists.");
        }


        var identityResult = await _userManager.CreateAsync(applicationUser, request.Password);
        if (!identityResult.Succeeded)
            throw new RegisterIdentityUserException(string.Join(',', identityResult.Errors.Select(e => e.Description)));

        var roleResult = await _userManager.AddToRolesAsync(
            applicationUser,
            request.Roles ?? new List<string> { IdentityConstants.Role.User }
        );

        if (!roleResult.Succeeded)
        {
            var getUserToDelet = await _userManager.FindByIdAsync(applicationUser.Id.ToString());
            //throw new RegisterIdentityUserException(string.Join(',', roleResult.Errors.Select(e => e.Description)));
            var deleteUser = await _userManager.DeleteAsync(getUserToDelet);
            if (deleteUser.Succeeded)
            {
                throw new BadRequestException("خدث خطاء  في اضافة الصلاحيات تأكد من الصلاحيات المضافة");
            }
        }
        if (request.Permissions != null)
        {
            foreach (var item in request.Permissions)
            {
                var screenId = await _screenContext.FindScreenByHashName(item.hashName);
                if (screenId == null)
                {
                    var deletUser = await _userManager.DeleteAsync(applicationUser);
                    if (!deletUser.Succeeded)
                    {
                        throw new BadRequestException(string.Join(',', deletUser.Errors.Select(e => e.Description)));
                    }
                    throw new BadRequestException("الشاشة غير موجودة");
                }
                var userScreen = new UserScreen()
                {
                    Id = Guid.NewGuid(),
                    UserId = applicationUser.Id,
                    ScreenId = screenId.Id,
                    Menu = item.Menu,
                    IsView = item.IsView,
                    IsDetail = item.IsDetail,
                    IsCreated = item.IsCreated,
                    IsUpdated = item.IsUpdated,
                    IsDeleted = item.IsDeleted,
                    IsPrint = item.IsPrint,
                    IsExport = item.IsExport,
                    IsImport = item.IsImport,
                };
                await _screenContext.CreateUserScreenAsync(userScreen);
            }
        }
        throw new SuccessException(applicationUser.Id);
    }
}