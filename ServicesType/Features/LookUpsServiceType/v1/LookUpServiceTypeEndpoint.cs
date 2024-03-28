using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Categories.Features.LookUpsServiceType.v1
{
    public class LookUpServiceTypeController : Controller
    {
        private readonly ILogger<LookUpServiceTypeController> _logger;
        private readonly ICommandProcessor _send;
        public LookUpServiceTypeController(ILogger<LookUpServiceTypeController> logger, ICommandProcessor send)
        {
            _send = send;
            _logger = logger;
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "superAdmin,superUser,supercompanyadmin,Admin,company")]
        [Route("api/service_type/lookup/v1")]
        [ApiExplorerSettings(GroupName = "service_type")]
        [HttpGet]
        public async Task<ActionResult<LookUpServiceTypeResponse>> GetLookUpServiceTypeMeth()
        {
            ///<summary>
            /// Query for getting users 
            /// </summary>
            var result = await _send.SendAsync(new LookUpServiceType());
            return Ok(result);
        }
    }
}
