using Microsoft.AspNetCore.Mvc;

using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.MaintenanceCenters.Features.UpdateCenters.v1;

public class UpdateCenterController : Controller
{
    private readonly ILogger<UpdateCenterController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public UpdateCenterController(ILogger<UpdateCenterController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
        //_cancellationToken = cancellationToken;
    }

    [Route("api/centers/update/v1")]
    [ApiExplorerSettings(GroupName = "centers")]
    [HttpPut]
    public async Task<ActionResult<UpdateCenterRequest>> UpdateCenterMeth([FromBody] UpdateCenterRequest request)
    {

        var result = await _sender.SendAsync(new UpdateCenter(request));
        return Ok(result);
    }
}
