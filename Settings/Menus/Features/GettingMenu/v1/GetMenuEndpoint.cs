using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Settings.Menus.Model;

namespace QuickFix.Settings.Menus.Features.GettingMenu.v1
{
    public class GetMenuController : Controller
    {
        private readonly ILogger<GetMenuController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetMenuController(ILogger<GetMenuController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "superadmin,superuser,company,companyadmin,companyuser")]
        [Route("api/setting/menu/get_all/v1")]
        [ApiExplorerSettings(GroupName = "setting")]
        [HttpGet]
        public async Task<ActionResult<IHasMenuItems>> GetMenuMeth(Guid? Id)
        {
            var result = await _sender.SendAsync(new GetMenu(Id));
            return Ok(result);
        }
    }
}
