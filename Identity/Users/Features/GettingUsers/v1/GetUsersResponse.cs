using System;
using QuickFix.Identity.Users.Models.DTOs;

namespace QuickFix.Identity.Users.Features.GettingUsers.v1
{
    public record GetUsersResponse(IEnumerable<IdentityUserDto> Users);
    
}
