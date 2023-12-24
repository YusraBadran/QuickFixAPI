using System;
using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickFix.Identity.Shared.Models;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Persistence.EfCore;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Identity.Shared.Exceptions;


public static class UserManagerExtensions
{
    public static async Task<ApplicationUser?> FindUserByIdAsync(
        this UserManager<ApplicationUser> userManager,
        Guid userId
    )
    {
        return await userManager.Users
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }
    public static async Task<IReadOnlyList<ApplicationUser>> GetAllUsersAsync(
        this UserManager<ApplicationUser> userManager
    ){
        return await userManager.Users.ToListAsync();   
    }
     public static async Task<IReadOnlyList<ApplicationUser>> FindAllUserWithRoleAsync(
        this UserManager<ApplicationUser> userManager
    )
    {
        return await userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
    }


    public static async Task<ListResultModel<TResult>> FindAllUsersByPageAsync<TResult>(
        this UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IPageRequest request,
        CancellationToken cancellationToken
    )
        where TResult : notnull
    {
        return await userManager.Users
            .OrderByDescending(x => x.CreatedAt)
            .ApplyIncludeList(request.Includes)
            .ApplyFilter(request.Filters)
            .AsNoTracking()
            .ApplyPagingAsync<ApplicationUser, TResult>(
                mapper.ConfigurationProvider,
                request.Page,
                request.PageSize,
                cancellationToken: cancellationToken
            );
    }
    public static async Task<ApplicationUser?> FindUserWithRoleByIdAsync(
        this UserManager<ApplicationUser> userManager,
        Guid userId
    )
    {
        return await userManager.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public static async Task<ApplicationUser?> FindUserWithRoleByUserNameAsync(
        this UserManager<ApplicationUser> userManager,
        string userName
    )
    {
        return await userManager.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.UserName == userName);
    }

    public static async Task<ApplicationUser?> FindUserWithRoleByEmailAsync(
        this UserManager<ApplicationUser> userManager,
        string email
    )
    {
        return await userManager.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.Email == email);
    }
}
