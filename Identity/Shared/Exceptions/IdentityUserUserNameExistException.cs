using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;
namespace QuickFix.Identity.Shared.Exceptions
{
    public class IdentityUserUserNameExistException : ConflictException
    {
        public IdentityUserUserNameExistException(string UserNameOrEmail)
            : base($" اسم المستخدم: '{UserNameOrEmail}' موجود مسبقاً")
        {
            Detail = new DataRespons
            {
                Message = $" اسم المستخدم: '{UserNameOrEmail}' موجود مسبقاً",
                StatusCode = (int)HttpStatusCode.Conflict
            };
            StatusCode = HttpStatusCode.Conflict;
        }

    }
}
