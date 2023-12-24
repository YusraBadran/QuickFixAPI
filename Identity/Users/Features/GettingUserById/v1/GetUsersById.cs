using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Identity.Users.Features.GettingUserById.v1;

public record GetUsersById(Guid Id) :IQuery<GetUsersByIdResponse>;

public class Validate:AbstractValidator<GetUsersById>
{
    public Validate()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Required");
    }
}

public class GetUsersByIdHandler : IQueryHandler<GetUsersById, GetUsersByIdResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IMapper _mapper;

    public GetUsersByIdHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<GetUsersByIdResponse> Handle(GetUsersById request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindUserByIdAsync(request.Id);

        var userDto = _mapper.Map<IdentityUserDto>(user);

        return new GetUsersByIdResponse(userDto);
    }
}

