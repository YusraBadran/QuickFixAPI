using System;
using QuickFix.Identity.Shared.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1;

public record UpdateUserRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public TypeStates UserState { get; set; }
}

