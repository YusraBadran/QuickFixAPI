using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.GettingUesrByEmail.v1.Exceptions
{
    public class UserWithEmailNotFoundException : NotFoundException
    {
        public UserWithEmailNotFoundException(string email) : base($"User with email {email} not found")
        {
            StatusCode = HttpStatusCode.NotFound;
        }
        
    }
}
