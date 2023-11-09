using System;

namespace QuickFix.Identity.Identitys.Models
{
 public record GenerateJwtTokenResponse(string Token, DateTime ExpireAt);
}
