using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.Register.v1.Exceptions
{
    public class RegisterExistUserNameException : ConflictException
    {
        public RegisterExistUserNameException(string UserName) : base($"UserName {UserName} already exist.")
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
