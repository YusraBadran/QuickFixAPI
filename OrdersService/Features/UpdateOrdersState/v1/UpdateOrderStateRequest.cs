using QuickFix.Shared.Module;

namespace QuickFix.OrdersService.Features.UpdateOrdersState.v1;

public record UpdateOrderStateRequest
{
    public Guid Id { get; set; }
    public TypeStates status { get; set; }
}
