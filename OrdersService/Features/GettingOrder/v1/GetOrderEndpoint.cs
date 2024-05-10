using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using QuickFix.Security.ApiKey.Authorization;

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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    [Route("api/orders/get_all/v1")]
    [ApiExplorerSettings(GroupName = "orders")]
    [HttpGet]
    public async Task<ActionResult<GetOrderRespons>> GetAllOrderMeth()
    {
        var result = await _sender.SendAsync(new GetOrder());
        return Ok(result);
    }
}