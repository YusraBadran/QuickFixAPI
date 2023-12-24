using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Features.GettingUsersByPage.v1;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Identity.Users.Features.GettingAllUserByPage.v1;

    public record GetUsersByPage:ListQuery<GetUsersByPageResponse>;
    ///<summary>
/// Get Users Validator
/// </summary>
public class Validator : AbstractValidator<GetUsersByPage>
{
    public Validator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page should at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize should at least greater than or equal to 1.");
    }
}

public class GetUsersByPageHandler : IQueryHandler<GetUsersByPage, GetUsersByPageResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;   
    public GetUsersByPageHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<GetUsersByPageResponse> Handle(GetUsersByPage request, CancellationToken cancellationToken)
    {
        var result = await _userManager.FindAllUsersByPageAsync<IdentityUserDto>(_mapper, request, cancellationToken);
        return new GetUsersByPageResponse(result);
    }
}

