using System;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
    public class InvalidRefreshTokenException : BadRequestException
{
    public InvalidRefreshTokenException(Shared.Models.RefreshTokens? refreshToken)
        : base($"refresh token {refreshToken?.Token} is invalid!") { }
}

}
