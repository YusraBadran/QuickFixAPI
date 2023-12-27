using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class UpdateUserPhoneUnavailableOrNotExists : ConflictException
    {
        public UpdateUserPhoneUnavailableOrNotExists(string PhoneNumber)
            : base($"UpdateUser with id {PhoneNumber} was not found.")
        {
             StatusCode = HttpStatusCode.Conflict;
        }
    }
}
