using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;
namespace QuickFix.Identity.Identitys.Exceptions;

public class PasswordIsInvalidException : IdentityException
{
    public PasswordIsInvalidException()
        : base($" كلمة المرور غير صحيحة.")
    {
        Detail = new DataRespons
        {
            Message = $" كلمة المرور غير صحيحة.",
            StatusCode = (int)HttpStatusCode.Conflict
        };
        StatusCode = HttpStatusCode.Conflict;
    }
}
