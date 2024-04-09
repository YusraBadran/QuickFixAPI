using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.MaintenanceCenters.Features.GettingCenters.v1
{
    public class GetCenterController : Controller
    {
        private readonly ILogger<GetCenterController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetCenterController(ILogger<GetCenterController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/centers/get_all/v1")]
        [ApiExplorerSettings(GroupName = "centers")]
        [HttpGet]
        public async Task<ActionResult<GetCenterRespons>> CreateCenterMeth()
        {
            var result = await _sender.SendAsync(new GetCenter());
            return Ok(result);
        }
    }
}
