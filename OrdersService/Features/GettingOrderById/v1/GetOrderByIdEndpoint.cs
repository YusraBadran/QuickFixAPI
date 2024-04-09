using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;


namespace QuickFix.OrdersService.Features.GettingOrderById.v1;

public class GetOrderByIdController : Controller
{
    private readonly ILogger<GetOrderByIdController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetOrderByIdController(ILogger<GetOrderByIdController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/orders/get_by_id/v1")]
    [ApiExplorerSettings(GroupName = "orders")]
    [HttpGet]
    public async Task<ActionResult<GetOrderByIdRespons>> GetOrderByIdMeth(Guid Id)
    {
        var result = await _sender.SendAsync(new GetOrderById(Id));
        return Ok(result);
    }
}