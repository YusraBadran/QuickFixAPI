using System;
using System.ComponentModel;
using QuickFix.Identity.Shared.Models;

namespace QuickFix.Identity.Users.Features.Register.v1;

public record RegisterUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public UserState UserState { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public IEnumerable<string>? Roles { get; set; }
    public IEnumerable<UserScreenPermission>? Permissions { get; set; }
}
public record RegisterUsersRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public UserState UserState { get; set; }
    public IEnumerable<string>? Roles { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
public class UserScreenPermission
{
    public string hashName { get; set; }
    public bool? Menu { get; set; } = true;
    public bool? IsView { get; set; } = false;
    public bool? IsDetail { get; set; } = false;
    public bool? IsCreated { get; set; } = false;
    public bool? IsUpdated { get; set; } = false;
    public bool? IsDeleted { get; set; } = false;
    public bool? IsPrint { get; set; } = false;
    public bool? IsExport { get; set; } = false;
    public bool? IsImport { get; set; } = false;
}