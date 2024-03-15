using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.Categories.Exceptions
{
    public class CategoryAlreadyExistException : ConflictException
    {
        public CategoryAlreadyExistException() : base(" الفئة موجودة بالفعل")
        {
            Detail = new DataRespons
            {
                Message = $" الفئة موجودة بالفعل",
                StatusCode = (int)HttpStatusCode.Conflict,
            };
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
