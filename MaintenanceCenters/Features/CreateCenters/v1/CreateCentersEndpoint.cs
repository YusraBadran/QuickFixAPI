using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.MaintenanceCenters.Features.CreateCenters.v1
{
    public class CreateCentersController : Controller
    {
        private readonly ILogger<CreateCentersController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public CreateCentersController(ILogger<CreateCentersController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/centers/create/v1")]
        [ApiExplorerSettings(GroupName = "centers")]
        [HttpPost]
        public async Task<ActionResult<CreateCentersRequest>> CreateCenterMeth([FromBody] CreateCentersRequest request)
        {
            var result = await _sender.SendAsync(new CreateCenter(request));
            return Ok(result);
        }
    }
}
