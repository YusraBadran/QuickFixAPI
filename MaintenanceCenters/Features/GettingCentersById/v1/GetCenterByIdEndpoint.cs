using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.MaintenanceCenters.Features.GettingCentersById.v1;

public class GetCenterByIdController : Controller
{
    private readonly ILogger<GetCenterByIdController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetCenterByIdController(ILogger<GetCenterByIdController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/centers/get_by_id/v1")]
    [ApiExplorerSettings(GroupName = "centers")]
    [HttpGet]
    public async Task<ActionResult<GetCenterByIdRespons>> GetCategoryByIdMeth(Guid Id)
    {
        var result = await _sender.SendAsync(new GetCenterById(Id));
        return Ok(result);
    }
}
