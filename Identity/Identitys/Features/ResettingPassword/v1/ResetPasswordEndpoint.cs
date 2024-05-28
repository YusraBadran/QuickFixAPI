using QuickFix.Security.ApiKey.Authorization;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Identity.Identitys.Features.ResettingPassword.v1
{
    public class ResetPasswordController : Controller
    {
        private readonly ILogger<ResetPasswordController> _logger;
        private readonly ICommandProcessor _sender;
        public ResetPasswordController(ILogger<ResetPasswordController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "superadmin,superuser,company,user")]
        [Route("api/identity/reset_password/v1")]
        [ApiExplorerSettings(GroupName = "identity")]
        [HttpPost]
        public async Task<ActionResult<ResetPasswordRequest>> ResetPasswordMeth([FromBody] ResetPasswordRequest request)
        {

            var result = await _sender.SendAsync(new ResetPassword(request));

            return Ok(result);

        }
    }
}
