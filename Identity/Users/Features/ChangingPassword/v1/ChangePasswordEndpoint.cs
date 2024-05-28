using QuickFix.Security.ApiKey.Authorization;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Identity.Users.Features.ChangingPassword.v1
{
    public class ChangePasswordController : Controller
    {
        private readonly ILogger<ChangePasswordController> _logger;
        private readonly ICommandProcessor _sender;
        public ChangePasswordController(ILogger<ChangePasswordController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "superadmin,superuser,company,user")]
        [Route("api/user/change_password/v1")]
        [ApiExplorerSettings(GroupName = "user")]
        [HttpPost]
        public async Task<ActionResult<ChangePasswordRequest>> ResetPasswordMeth([FromBody] ChangePasswordRequest request)
        {

            var result = await _sender.SendAsync(new ChangePassword(request));

            return Ok(result);

        }
    }
}
