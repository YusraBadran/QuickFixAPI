using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.DeleteUsers.v1.Exceptions
{
    public class DeleteUserNotFoundException : NotFoundException
    {
        public DeleteUserNotFoundException(Guid id) : base($" Falied To  Delete {id} User ")
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}

