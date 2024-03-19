using System;

namespace QuickFix.Identity.Identitys.Features.RefreshingToken.v1
{
    public record class RefreshTokenRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
