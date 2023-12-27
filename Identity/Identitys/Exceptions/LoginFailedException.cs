using System;

using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
   public class LoginFailedException : AppException
{
    public LoginFailedException(string userNameOrEmail)
        : base($"Login failed for username: {userNameOrEmail}")
    {
        UserNameOrEmail = userNameOrEmail;
    }

    public string UserNameOrEmail { get; }
}
}
