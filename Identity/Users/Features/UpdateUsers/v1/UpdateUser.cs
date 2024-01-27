using System;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1;
public record UpdateUser : UpdateUserRequest, ITxUpdateCommand
{
    public UpdateUser(UpdateUserRequest request) : base(request)
    { }
}

public class Validate : AbstractValidator<UpdateUser>
{
    public Validate()
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
    }
}

public class UpdateUserHandler : ICommandHandler<UpdateUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IMapper _mapper;

    public UpdateUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    //Unit -> it just use for testing success or not 
    public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var User = await _userManager.FindByIdAsync(request.Id.ToString());
        if (User == null)
        {
            throw new UpdateUserNotFoundException(request.Id);
        }
        var EmailExist = await _userManager.FindByEmailAsync(request.Email.ToLower());
        if (EmailExist != null)
        {
            if (EmailExist.Email != null && EmailExist.Id != request.Id)
            {
                throw new UpdateUserEmailExistException(request.Email.ToLower());
            }
        }

        var UserNameExist = await _userManager.FindByNameAsync(request.UserName);
        if (UserNameExist != null)
        {
            if (UserNameExist.UserName != null && UserNameExist.Id != request.Id)
            {
                throw new UpdateUserFindByNameException(request.UserName);
            }
        }

        var PhoneNumberExist = await _userManager.FindByPhoneNumberAsync(request.PhoneNumber);
        if (PhoneNumberExist != null)
        {
            if (PhoneNumberExist.PhoneNumber != null && PhoneNumberExist.Id != request.Id)
            {
                throw new UpdateUserPhoneUnavailableOrNotExists(request.PhoneNumber);
            }
        }
        User.FirstName = request.FirstName;
        User.LastName = request.LastName;
        User.UserName = request.UserName;
        User.Email = request.Email;
        User.PhoneNumber = request.PhoneNumber;
        User.UserState = request.UserState;
        var result = await _userManager.UpdateAsync(User);
        if (!result.Succeeded)
        {
            throw new UpdateUserFailedException(request.UserName);
        }

        throw new SuccessUpdateUserException((Guid)request.Id);
    }

}

