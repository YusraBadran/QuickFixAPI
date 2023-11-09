
using System;
using System.Net;
using QuickFix.Exceptions.Types;

namespace QuickFix.Identity.Identitys.Exceptions
{
   public class PhoneNumberNotConfirmedException : AppException
{
    public PhoneNumberNotConfirmedException(string phone)
        : base($"The phone number '{phone}' is not confirmed yet.", HttpStatusCode.UnprocessableEntity) { }
}
}
