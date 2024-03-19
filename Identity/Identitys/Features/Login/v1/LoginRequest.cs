using System;

namespace QuickFix.Identity.Identitys.Features.Login.v1
{
    public record class LoginRequest
    {
        public string UserNameOrEmail { get; set; }
        public string password { get; set; }
        public bool Remember { get; set; }
    }
}
