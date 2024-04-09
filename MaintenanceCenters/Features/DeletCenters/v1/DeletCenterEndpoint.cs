using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.MaintenanceCenters.Features.DeletCenters.v1;

public class DeletCentersController : Controller
{
    private readonly ICommandProcessor _sender;
    private readonly ILogger<DeletCentersController> _logger;
    private readonly CancellationToken _cancellationToken;
    public DeletCentersController(ICommandProcessor sender, ILogger<DeletCentersController> logger)
    {
        _sender = sender;
        _logger = logger;
        //_cancellationToken = new CancellationToken();
    }
    [Route("api/centers/delete/v1")]
    [ApiExplorerSettings(GroupName = "centers")]
    [HttpDelete]
    public async Task<IActionResult> DeletCentersMath(Guid Id)
    {

        var respons = await _sender.SendAsync(new DeletCenterss(Id));
        return Ok(respons);
    }
    [Route("api/centers/delete/v1")]
    [ApiExplorerSettings(GroupName = "centers")]
    [HttpPost]
    public async Task<IActionResult> PostDeletCentersMath(Guid Id)
    {

        var respons = await _sender.SendAsync(new DeletCenterss(Id));
        return Ok(respons);
    }
}
