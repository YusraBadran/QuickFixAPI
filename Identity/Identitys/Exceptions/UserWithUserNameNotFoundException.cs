using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;
namespace QuickFix.Identity.Identitys.Exceptions
{
    public class UserWithUserNameNotFoundException : NotFoundException
    {
        public UserWithUserNameNotFoundException(string userName)
     : base($"User with userName: '{userName}' not found.")
        {
            Detail = new DataRespons
            {
                Message = $"User with userName: '{userName}' not found.",
                StatusCode = (int)HttpStatusCode.NotFound
            };
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
