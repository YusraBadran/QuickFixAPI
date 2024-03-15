using System;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.ServicesType.Features.CreateServicesType.v1
{
    public class CreateServiceTypeController : Controller
    {
        private readonly ILogger<CreateServiceTypeController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public CreateServiceTypeController(ILogger<CreateServiceTypeController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
            //_cancellationToken = cancellationToken;
        }

        [Route("api/service_type/create/v1")]
        [ApiExplorerSettings(GroupName = "service_type")]
        [HttpPost]
        public async Task<ActionResult<CreateServiceTypeRequest>> CreateServiceType([FromBody] CreateServiceTypeRequest request)
        {

            var result = await _sender.SendAsync(new CreateServiceType(request));
            return Ok(result);
        }
    }
}
