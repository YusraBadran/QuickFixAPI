using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using QuickFix.Security.ApiKey.Authorization;

namespace QuickFix.OrdersService.Features.UpdateOrdersState.v1;

public class UpdateOrderStateController : Controller
{
    private readonly ICommandProcessor _sender;
    private readonly ILogger<UpdateOrderStateController> _logger;
    private readonly CancellationToken _cancellationToken;
    public UpdateOrderStateController(ICommandProcessor sender, ILogger<UpdateOrderStateController> logger)
    {
        _sender = sender;
        _logger = logger;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    [Route("api/orders/update/v1")]
    [ApiExplorerSettings(GroupName = "orders")]
    [HttpPut]
    public async Task<ActionResult<UpdateOrderStateRequest>> UpdateOrderStateMeth([FromBody] UpdateOrderStateRequest request)
    {

        var reque = await _sender.SendAsync(new UpdateOrderState(request));
        return Ok(reque);
    }
}
