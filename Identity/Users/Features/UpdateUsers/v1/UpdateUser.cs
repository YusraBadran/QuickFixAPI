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
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is Required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is Required");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is Required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email Address is Not Valid");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is Required");
        RuleFor(x => x.UserState).NotEmpty().WithMessage("UserState is Required");
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
        var EmailExist = await _userManager.FindByEmailAsync(request.Email);
        if (EmailExist.Email != null && EmailExist.Id != request.Id)
        {
            throw new UpdateUserExistEmailException(request.Email);
        }

        var UserNameExist = await _userManager.FindByNameAsync(request.UserName);
        if (UserNameExist.UserName != null && UserNameExist.Id != request.Id)
        {
            throw new UpdateUserFindByNameException(request.UserName);
        }

        var PhoneNumberExist = await _userManager.FindByPhoneNumberAsync(request.PhoneNumber);
        if (PhoneNumberExist.PhoneNumber != null && PhoneNumberExist.Id != request.Id)
        {
            throw new UpdateUserPhoneUnavailableOrNotExists(request.PhoneNumber);
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

             var success = new SuccessRequest()
            {
                Id = User.Id,
                StatusCode = 200,
                Message = "user updated successfully"
            };
            throw new SuccessUpdateUserException(success);
    }

}

