
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.Categories.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException() : base($"لم يتم العثور على الفئه")
        {
            Detail = new DataRespons
            {
                Message = $"لم يتم العثور على الفئه",
                StatusCode = (int)HttpStatusCode.Conflict,
            };
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}
