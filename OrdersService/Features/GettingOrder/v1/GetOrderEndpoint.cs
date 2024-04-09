using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.OrdersService.Features.GettingOrder.v1;

public class GetOrderController : Controller
{
    private readonly ILogger<GetOrderController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetOrderController(ILogger<GetOrderController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/orders/get_all/v1")]
    [ApiExplorerSettings(GroupName = "orders")]
    [HttpGet]
    public async Task<ActionResult<GetOrderRespons>> GetAllOrderMeth(Guid? Id)
    {
        var result = await _sender.SendAsync(new GetOrder(Id));
        return Ok(result);
    }
}