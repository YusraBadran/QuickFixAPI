using System;

namespace QuickFix.Identity.Shared.Models.Security.Jwt
{
 public record GenerateTokenResult(string AccessToken, DateTime ExpireAt);
}
