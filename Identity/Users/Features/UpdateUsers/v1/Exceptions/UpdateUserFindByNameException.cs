using System;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1.Exceptions
{
    public class UpdateUserFindByNameException : ConflictException
    {
        public UpdateUserFindByNameException(string UserName)
            : base($"UpdateUser with userName {UserName} was not found.")
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
