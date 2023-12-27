using System;
using System.Net;

namespace QuickFix.Shared.Exceptions.Types
{
      public class FailedException : CustomException
    {
        public FailedException(string message)
            : base(message)
        {
            StatusCode = HttpStatusCode.ExpectationFailed;
        }
    }
}
