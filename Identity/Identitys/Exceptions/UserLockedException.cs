using System;
using System.Net;
using QuickFix.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
   
public class UserLockedException : AppException
{
    public UserLockedException(string userId)
        : base($"userId '{userId}' has been locked.", HttpStatusCode.Forbidden) { }
}
}
