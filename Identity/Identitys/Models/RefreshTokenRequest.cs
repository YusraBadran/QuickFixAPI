using System;

namespace QuickFix.Identity.Identitys.Models
{
   public record class RefreshTokenRequest
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
}
