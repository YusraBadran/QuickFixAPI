using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;
namespace QuickFix.Identity.Identitys.Exceptions
{
    public class UserWithIdNotFoundException : NotFoundException
    {
        public UserWithIdNotFoundException(Guid id)
        : base($"User with Id: '{id}' not found.")
        {
            Detail = new DataRespons
            {
                Id = id,
                Message = $"User with Id: '{id}' not found.",
                StatusCode = (int)HttpStatusCode.NotFound
            };
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
