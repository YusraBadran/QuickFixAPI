using System;
using QuickFix.Identity.Shared.Models;

namespace QuickFix.Identity.Users.Models.RegisterUser;
public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string PhoneNumber,
    string Password,
    string ConfirmPassword
)
{
    public IEnumerable<string> Roles { get; init; } = new List<string> { IdentityConstants.Role.User };
}
