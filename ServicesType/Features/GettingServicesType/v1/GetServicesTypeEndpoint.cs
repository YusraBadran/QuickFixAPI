using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;
using StackExchange.Redis;

namespace QuickFix.ServicesType.Features.GettingServicesType.v1;

public class GetServicesTypeController : Controller
{
    private readonly ILogger<GetServicesTypeController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetServicesTypeController(ILogger<GetServicesTypeController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/service_type/get_all/v1")]
    [ApiExplorerSettings(GroupName = "service_type")]
    [HttpGet]
    public async Task<ActionResult<GetServicesTypeRespons>> CreateServiceTypeMeth()
    {
        var result = await _sender.SendAsync(new GetServicesType());
        return Ok(result);
    }
}
