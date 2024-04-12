using System;
using QuickFix.Identity.Shared.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Models.DTOs
{
    public class IdentityUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public TypeStates UserState { get; set; }

    }
}
