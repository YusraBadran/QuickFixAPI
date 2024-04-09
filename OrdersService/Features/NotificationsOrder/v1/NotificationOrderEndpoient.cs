using QuickFix.Security.ApiKey.Authorization;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.OrdersService.Features.NotificationsOrder.v1;

public class NotificationOrderController : Controller
{

    private readonly ILogger<NotificationOrderController> _logger;
    private readonly CancellationToken _cancellationToken;
    private readonly ICommandProcessor _sender;
    public NotificationOrderController(ICommandProcessor sender, ILogger<NotificationOrderController> logger)
    {
        _sender = sender;
        _logger = logger;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "superadmin,superuser,company,user")]
    [Route("api/orders/notification/v1")]
    [ApiExplorerSettings(GroupName = "orders")]
    [HttpGet]
    public async Task<ActionResult<NotificationOrderRespons>> GetNotificationsOrderMeth(Guid? Id)
    {

        var result = await _sender.SendAsync(new NotificationOrder(Id));
        return Ok(result);
    }
}

