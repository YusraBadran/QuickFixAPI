using System;
using System.Net;
using QuickFix.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
   public class EmailNotConfirmedException : AppException
{
    public EmailNotConfirmedException(string email)
        : base($"Email not confirmed for email address `{email}`", HttpStatusCode.UnprocessableEntity)
    {
        Email = email;
    }

    public string Email { get; }
}
}
