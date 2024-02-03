using Microsoft.AspNetCore.Http;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.Categories.Exceptions
{
    public class CategoryNameAlreadyExist : ConflictException
    {
        public CategoryNameAlreadyExist(string name) : base($"اسم الفئه {name} موجود مسبقاً")
        {
            Detail = new DataRespons
            {
                Message = $"اسم الفئه {name} موجود مسبقاً",
                StatusCode = (int)HttpStatusCode.Conflict,
            };
            StatusCode = HttpStatusCode.Conflict;
        }
    }
}

