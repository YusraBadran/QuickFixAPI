using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.Identity.Identitys.Exceptions;

// https://stackoverflow.com/questions/36283377/http-status-for-email-not-verified
public class EmailNotConfirmedException : ConflictException
{
    public EmailNotConfirmedException(string email)
        : base($"Email not confirmed for email address `{email}`")
    {
        Detail = new DataRespons
        {
            Message = $"Email not confirmed for email address `{email}`",
            StatusCode = (int)HttpStatusCode.Conflict
        };
        StatusCode = HttpStatusCode.Conflict;
    }
}
