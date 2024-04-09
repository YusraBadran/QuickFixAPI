using QuickFix.Shared.Core.Queries;
using QuickFix.OrdersService.Models.DTOs;

namespace QuickFix.OrdersService.Features.GettingOrdersByPage.v1
{
    public record GetOrderByPageRespons(ListResultModel<OrdersDto> order);

}
