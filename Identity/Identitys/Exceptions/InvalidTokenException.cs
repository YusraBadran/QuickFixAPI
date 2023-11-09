using System;
using System.Security.Claims;
using QuickFix.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
public class InvalidTokenException : AppException
{
    public InvalidTokenException(ClaimsPrincipal? claimsPrincipal)
        : base("access_token is invalid!") { }
}
}
