using System;
using QuickFix.Identity.Users.Models.DTOs;

namespace QuickFix.Identity.Users.Models.GetUserByEmail
{
    public record GetUserByEmailResponse(IdentityUserDto? UserIdentity);

}
