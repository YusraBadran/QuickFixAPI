using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class UpdateUserExistEmailException : ConflictException
    {
        public UpdateUserEmailExistException(string Email) : base($" البريد الإلكتروني '{Email}' موجود مسبقاً")
        {
            Detail = new DataRespons
            {
                Message = $" البريد الإلكتروني '{Email}' موجود مسبقاً",
                StatusCode = (int)HttpStatusCode.Conflict
            };
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}

