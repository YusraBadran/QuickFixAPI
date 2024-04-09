using QuickFix.OrdersService.Models.DTOs;

namespace QuickFix.OrdersService.Features.GettingOrder.v1
{
    public record GetOrderRespons(IEnumerable<OrdersDto> order);
}
