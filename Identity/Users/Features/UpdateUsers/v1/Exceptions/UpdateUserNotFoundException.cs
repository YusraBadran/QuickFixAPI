using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;


namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class UpdateUserNotFoundException : NotFoundException
    {
        public UpdateUserNotFoundException(Guid Id)
            : base($"UpdateUser with id {Id} was not found.")
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }

}
