using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;
namespace QuickFix.Identity.Identitys.Exceptions
{
    public class UserWithEmailNotFoundException : NotFoundException
    {
        public UserWithEmailNotFoundException(string email)
     : base($"المستخدم مع البريد الإلكتروني: '{email}' غير موجود.")
        {
            Detail = new DataRespons
            {
                Message = $"المستخدم مع البريد الإلكتروني: '{email}' غير موجود.",
                StatusCode = (int)HttpStatusCode.NotFound
            };
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
