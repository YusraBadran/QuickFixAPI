
using System.Net;

namespace QuickFix.Exceptions.Types
{
    public class ConflictException:CustomException
    {
        public ConflictException(string message):base(message)
        {
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
