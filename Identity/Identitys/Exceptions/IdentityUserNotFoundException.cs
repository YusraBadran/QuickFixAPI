using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
public class IdentityUserNotFoundException : AppException
{
public IdentityUserNotFoundException(string UserNameOrEmail)
    : base($"User with email or username: '{UserNameOrEmail}' not found.", HttpStatusCode.NotFound) { }

    public IdentityUserNotFoundException(Guid id)
        : base($"User with id: '{id}' not found.", HttpStatusCode.NotFound) { }
}

}
