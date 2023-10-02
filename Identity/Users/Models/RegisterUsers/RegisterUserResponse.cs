using System;
using QuickFix.Identity.Users.Models.DTOs;

namespace QuickFix.Identity.Users.Models.RegisterUser
{
    public record RegisterUserResponse(IdentityUserDto? UserIdentity);
}
