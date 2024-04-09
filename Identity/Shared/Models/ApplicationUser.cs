using System;
using Microsoft.AspNetCore.Identity;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Shared.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime? LastLoggedInAt { get; set; }

        public virtual ICollection<RefreshTokens> RefreshTokens { get; set; } = default!;
        // public virtual ICollection<AccessToken> AccessTokens { get; set; } = default!;
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = default!;
        public TypeStates UserState { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
