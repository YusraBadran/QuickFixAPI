using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.Identity.Shared.Exceptions;


public class IdentityUserNotFoundException : NotFoundException
{
    public IdentityUserNotFoundException(string UserNameEmail)
        : base($" لا يوجد مستخدم بالاسم او البريد الاكتروني: '{UserNameEmail}'")
    {
        Detail = new DataRespons
        {
            Message = $" لا يوجد مستخدم بالاسم او البريد الاكتروني: '{UserNameEmail}'",
            StatusCode = (int)HttpStatusCode.Conflict
        };
        StatusCode = HttpStatusCode.Conflict;
    }

    public IdentityUserNotFoundException(Guid id)
        : base($" المستخدم بالرقم التعريفي: '{id}' غير موجود")
    {
        Detail = new DataRespons
        {
            Message = $" المستخدم بالرقم التعريفي: '{id}' غير موجود",
            StatusCode = (int)HttpStatusCode.NotFound
        };
        StatusCode = HttpStatusCode.NotFound;
    }
}

