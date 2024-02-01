using System;
using System.Net;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Exceptions
{
    public class ServiceTypeNameAlreadyExist : ConflictException
    {
        public ServiceTypeNameAlreadyExist(string name) : base($"اسم الخدمة {name} موجود مسبقاً")
        {
            Detail = new DataRespons
            {
                Message = $"اسم الخدمة {name} موجود مسبقاً",
                StatusCode = (int)HttpStatusCode.Conflict,
            };
            StatusCode = HttpStatusCode.Conflict;
        }
    }



}
