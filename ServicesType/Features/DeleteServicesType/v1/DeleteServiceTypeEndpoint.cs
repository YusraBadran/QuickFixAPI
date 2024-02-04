using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.ServicesType.Features.DeleteServicesType.v1
{
    public class DeleteServiceTypeController : Controller
    {
        private readonly ICommandProcessor _sender;
        private readonly ILogger<DeleteServiceTypeController> _logger;
        private readonly CancellationToken _cancellationToken;
        public DeleteServiceTypeController(ICommandProcessor sender, ILogger<DeleteServiceTypeController> logger)
        {
            _sender = sender;
            _logger = logger;
            //_cancellationToken = new CancellationToken();
        }
        [Route("api/service_type/delete/v1")]
        [ApiExplorerSettings(GroupName = "service_type")]
        [HttpDelete]
        public async Task<IActionResult> DleteServiceTypeMath(Guid Id)
        {

            var respons = await _sender.SendAsync(new DeleteServiceType(Id));
            return Ok(respons);
        }
    }
}
