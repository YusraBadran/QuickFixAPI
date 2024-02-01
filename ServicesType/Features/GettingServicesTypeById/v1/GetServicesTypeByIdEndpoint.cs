using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;
using StackExchange.Redis;

namespace QuickFix.ServicesType.Features.GettingServicesTypeById.v1
{
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
        [Route("api/serviceType/get_by_id/v1")]
        [ApiExplorerSettings(GroupName = "serviceType")]
        [HttpGet]
        public async Task<ActionResult<GetServicesTypeByIdRespons>> GetServiceTypeByIdMeth(Guid Id)
        {
            var result = await _sender.SendAsync(new GetServicesTypeById(Id));
            return Ok(result);
        }
    }
}
