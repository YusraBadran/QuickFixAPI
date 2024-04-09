using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using System.Net;

namespace QuickFix.OrdersService.Exceptions
{
    public class NotFoundOrderWithIdException : NotFoundException
    {
        public NotFoundOrderWithIdException(Guid orderId)
            : base($" الطلب بالرقم التعريفي : '{orderId}' غير موجود .")
        {
            Detail = new DataRespons
            {
                Id = orderId,
                Message = $" الطلب بالرقم التعريفي : '{orderId}' غير موجود .",
                StatusCode = (int)HttpStatusCode.NotFound
            };
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
