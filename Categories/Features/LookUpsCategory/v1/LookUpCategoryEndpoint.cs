using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Categories.Features.LookUpsCategory.v1
{
    public class LookUpCategoryController : Controller
    {
        private readonly ILogger<LookUpCategoryController> _logger;
        private readonly CancellationToken _cancellationToken;
        private readonly ICommandProcessor _send;
        public LookUpCategoryController(ILogger<LookUpCategoryController> logger, ICommandProcessor send)
        {
            _send = send;
            _logger = logger;
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "superAdmin,superUser,supercompanyadmin,Admin,company")]
        [Route("api/category/lookup/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpGet]
        public async Task<ActionResult<LookUpCategoryRespons>> GetUsersMeth()
        {
            ///<summary>
            /// Query for getting users 
            /// </summary>
            var result = await _send.SendAsync(new LookUpCategory());
            return Ok(result);
        }
    }
}
