using System;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Features.DeleteUsers.v1.Exceptions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;


namespace QuickFix.Identity.Users.Features.DeleteUsers.v1;

public record DeleteUser(Guid Id) : ITxUpdateCommand
{
    // public DeleteUser(DeleteUserRequest request) : base(request)
    // { }
}


public class Validate : AbstractValidator<DeleteUser>
{
    public Validate()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");

    }
}

public class DeleteUserHandler : ICommandHandler<DeleteUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IMapper _mapper;

    public DeleteUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var UserExist = await _userManager.FindByIdAsync(request.Id.ToString());
        if (UserExist == null)
        {
            throw new DeleteUserNotFoundException(request.Id);
        }
        var result = await _userManager.DeleteAsync(UserExist);



        throw new SuccessException(UserExist.Id);
    }
}