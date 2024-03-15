using System.Security.Claims;

namespace QuickFix.Security.Jwt;

public interface ISecurityContextAccessor
{
    string UserId { get; }
    IEnumerable<Claim> Role { get; }
    string JwtToken { get; }
    bool IsAuthenticated { get; }
}
