using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.Register.v1.Exceptions
{
    public class RegisterExistEmailException : ConflictException
    {
        public RegisterExistEmailException(string Email) : base($"Email {Email} already exist.")
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}

