using System;
using QuickFix.Identity.Users.Models.DTOs;

namespace QuickFix.Identity.Users.Features.GettingUserById.v1;
    public record GetUsersByIdResponse(IdentityUserDto User);

