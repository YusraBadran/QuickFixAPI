using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.ServicesType.Features.UpdateServicesType.v1
{
    public class UpdateServiceTypeController : Controller
    {
        private readonly ILogger<UpdateServiceTypeController> _logger;
        private readonly CancellationToken _cancellationToken;
        private readonly ICommandProcessor _sender;
        public UpdateServiceTypeController(ILogger<UpdateServiceTypeController> logger, ICommandProcessor sender)
        {
            _sender = sender;
            _logger = logger;
        }
        [Route("api/service_type/update/v1")]
        [ApiExplorerSettings(GroupName = "serviceType")]
        [HttpPut]
        public async Task<ActionResult<UpdateServiceTypeRequest>> UpdateCompanyMeth([FromBody] UpdateServiceTypeRequest? request)
        {

            var result = await _sender.SendAsync(new UpdateServiceType(request));
            return Ok(result);
        }
    }
}
