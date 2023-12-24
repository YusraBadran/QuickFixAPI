using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Identity.Users.Features.GettingUsers.v1;
  public record GetUsers : ITxCommand<GetUsersResponse>;

  public class GetUserHandler : ICommandHandler<GetUsers,GetUsersResponse>
  {
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IMapper _mapper;

    public GetUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
      _userManager = userManager;
      _mapper = mapper;
    }

    public async Task<GetUsersResponse> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        var result = await _userManager.GetAllUsersAsync();

        var userMap = _mapper.Map<IEnumerable<IdentityUserDto>>(result);

        return new GetUsersResponse(userMap);
    }
}
