using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class UpdateUserExistEmailException : ConflictException
    {
        public UpdateUserExistEmailException(string Email)
            : base($"UpdateUser with email {Email} was not found.")
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}

