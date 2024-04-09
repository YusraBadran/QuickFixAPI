using QuickFix.Security.ApiKey.Authorization;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.OrdersService.Features.CreateOrders.v1;

public class CreateOdrerController : Controller
{
    private readonly ILogger<CreateOdrerController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public CreateOdrerController(ILogger<CreateOdrerController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
    [Route("api/orders/create/v1")]
    [ApiExplorerSettings(GroupName = "orders")]
    [HttpPost]
    public async Task<ActionResult<CreateOrderRequest>> CreateCategoryMeth([FromBody] CreateOrderRequest request)
    {

        var result = await _sender.SendAsync(new CreateOrder(request));
        return Ok(result);
    }
}
