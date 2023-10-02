using System;
using Microsoft.AspNetCore.Identity;

namespace QuickFix.Identity.Shared.Models
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser? User { get; set; }
        public virtual ApplicationRole? Role { get; set; }
    }
}
