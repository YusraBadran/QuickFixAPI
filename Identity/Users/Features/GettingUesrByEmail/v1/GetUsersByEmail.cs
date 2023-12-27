using System;
using Ardalis.GuardClauses;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Features.GettingUesrByEmail.v1.Exceptions;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Identity.Users.Models.GetUserByEmail;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Identity.Users.Features.GettingUserByEmail.v1;
public record GetUserByEmail(string Email) : IQuery<GetUserByEmailResponse>;
public class Validate : AbstractValidator<GetUserByEmail>
{
    public Validate()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email Address is Not Valid");
    }
}

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmail, GetUserByEmailResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetUserByEmailHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = Guard.Against.Null(userManager, nameof(userManager));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<GetUserByEmailResponse> Handle(GetUserByEmail request, CancellationToken cancellationToken)
    {
        var UserByEmail = await _userManager.FindByEmailAsync(request.Email);
        if (UserByEmail == null)
        {
            throw new UserWithEmailNotFoundException(request.Email);
        }

        var IdentityUser = await _userManager.FindUserWithRoleByEmailAsync(request.Email);

        var userDto = _mapper.Map<IdentityUserDto>(IdentityUser);

        return new GetUserByEmailResponse(userDto);
    }
}
