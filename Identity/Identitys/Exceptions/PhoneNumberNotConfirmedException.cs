using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.Identity.Identitys.Exceptions;

public class PhoneNumberNotConfirmedException : ConflictException
{
    public PhoneNumberNotConfirmedException(string phone)
        : base($"The phone number '{phone}' is not confirmed yet.")
    {
        Detail = new DataResponse
        {
            Message = $"The phone number '{phone}' is not confirmed yet.",
            StatusCode = (int)HttpStatusCode.Conflict
        };
        StatusCode = HttpStatusCode.Conflict;
    }
}
