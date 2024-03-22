using System;
using QuickFix.Identity.Shared.Models;

namespace QuickFix.Identity.Identitys.Features.Login.v1
{
     public record LoginResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public LoginData Data { get; set; }
    }
    public class LoginData
    {
        public Guid Id { get; set; }
        public string? AccessToken { get; set; }
        public string? FirstName { get; set; }
        public string? Username { get; set; }
        public string? RefreshToken { get; set; }
        // public List<IHasMenuItems> menu { get; set; }
    }
}
