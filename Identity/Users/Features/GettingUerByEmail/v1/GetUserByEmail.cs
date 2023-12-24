using System;
using Ardalis.GuardClauses;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Identity.Users.Models.GetUserByEmail;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Identity.Users.Features.GettingUerByEmail.v1;
public record GetUserByEmail(string email) : IQuery<GetUserByEmailResponse>;
public class Validate : AbstractValidator<GetUserByEmail>
{
    public Validate()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.email).NotEmpty().EmailAddress().WithMessage("Email Address is Not Valid");
    }
}

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmail , GetUserByEmailResponse>
{
    private readonly UserManager<ApplicationUser>   _userManager;
    private readonly IMapper _mapper;

    public GetUserByEmailHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = Guard.Against.Null(userManager, nameof(userManager));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
    }

    public async Task<GetUserByEmailResponse> Handle(GetUserByEmail query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));

        var IdentityUser = await _userManager.FindUserWithRoleByEmailAsync(query.email);

        var userDto = _mapper.Map<IdentityUserDto>(IdentityUser);

        return new GetUserByEmailResponse(userDto);
    }
}
