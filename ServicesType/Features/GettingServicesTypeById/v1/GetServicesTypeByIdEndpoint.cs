using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;
using StackExchange.Redis;

namespace QuickFix.ServicesType.Features.GettingServicesTypeById.v1;

public class GetServicesTypeByIdController : Controller
{
    private readonly ILogger<GetServicesTypeByIdController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetServicesTypeByIdController(ILogger<GetServicesTypeByIdController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/service_type/get_by_id/v1")]
    [ApiExplorerSettings(GroupName = "service_type")]
    [HttpGet]
    public async Task<ActionResult<GetServicesTypeByIdRespons>> GetServiceTypeByIdMeth(Guid Id)
    {
        var result = await _sender.SendAsync(new GetServicesTypeById(Id));
        return Ok(result);
    }
}
