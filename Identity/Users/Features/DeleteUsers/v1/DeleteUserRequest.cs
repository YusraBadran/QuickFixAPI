using System;
using QuickFix.Identity.Shared.Models;

namespace QuickFix.Identity.Users.Features.DeleteUsers.v1;

public record DeleteUserRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public UserState UserState { get; set; }
}

