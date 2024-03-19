using QuickFix.Shared.Exceptions.Types;
using System.Net;

namespace QuickFix.Identity.Identitys.Exceptions;

public class UserLockedException : AppException
{
    public UserLockedException(string userId)
        : base($"userId '{userId}' has been locked.", HttpStatusCode.Forbidden) { }
}
