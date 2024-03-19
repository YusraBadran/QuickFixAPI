using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;
namespace QuickFix.Identity.Shared.Exceptions
{
    public class IdentityUserEmailExistException : ConflictException
    {
        public IdentityUserEmailExistException(string Email)
            : base($" الببريد الاكتروني للمستخدم: '{Email}' موجود مسبقاً")
        {
            Detail = new DataRespons
            {
                Message = $" الببريد الاكتروني للمستخدم: '{Email}' موجود مسبقاً",
                StatusCode = (int)HttpStatusCode.Conflict
            };
            StatusCode = HttpStatusCode.Conflict;

        }
    }
}
