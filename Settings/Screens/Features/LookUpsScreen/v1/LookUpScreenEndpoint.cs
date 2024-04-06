using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Settings.Screens.Features.LookUpsScreen.v1
{
    public class LookUpScreenController : Controller
    {
        private readonly ILogger<LookUpScreenController> _logger;
        private readonly CancellationToken _cancellationToken;
        private readonly ICommandProcessor _send;
        public LookUpScreenController(ILogger<LookUpScreenController> logger, ICommandProcessor send)
        {
            _send = send;
            _logger = logger;
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "superAdmin,superUser,supercompanyadmin,Admin,company")]
        [Route("api/screen/lookup/v1")]
        [ApiExplorerSettings(GroupName = "setting")]
        [HttpGet]
        public async Task<ActionResult<LookUpScreenRespons>> GetUsersMeth()
        {
            ///<summary>
            /// Query for getting users 
            /// </summary>
            var result = await _send.SendAsync(new LookUpScreen());
            return Ok(result);
        }
    }
}
