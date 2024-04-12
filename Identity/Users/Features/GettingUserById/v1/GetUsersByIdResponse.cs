using System;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Settings.Screens.Model.DTOs;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.GettingUserById.v1;
public record GetUsersByIdResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? LastLoggedInAt { get; set; }
    public Guid? BranchId { get; set; }
    public IEnumerable<string>? Roles { get; set; }
    public TypeStates UserState { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool CompanyAccount { get; set; }
    public Guid? CompanyId { get; set; }
    public IEnumerable<UserScreenDTO> Permissions { get; set; }
}

