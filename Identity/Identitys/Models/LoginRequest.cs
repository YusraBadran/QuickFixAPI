using System;

namespace QuickFix.Identity.Identitys.Models
{
    public record class LoginRequest
    {
        public string UserNameOrEmail { get; set; }
        public string password { get; set; }
        public bool Remember { get; set; }
    }
}
