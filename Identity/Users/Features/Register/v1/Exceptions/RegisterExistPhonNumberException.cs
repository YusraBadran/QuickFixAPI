using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;

namespace QuickFix.Identity.Users.Features.Register.v1.Exceptions
{
    public class RegisterExistPhonNumberException : ConflictException
    {
        public RegisterExistPhonNumberException(string PhoneNumber) : base($"PhoneNumber {PhoneNumber} already exist.")
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
