using System;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Identity.Users.Features.GettingUsersByPage.v1;

    public record GetUsersByPageResponse(
        ListResultModel<IdentityUserDto> Users
    );

