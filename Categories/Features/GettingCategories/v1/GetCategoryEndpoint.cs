using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategories.v1
{
    public class GetCategoryController : Controller
    {
        private readonly ILogger<GetCategoryController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetCategoryController(ILogger<GetCategoryController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/category/get_all/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpGet]
        public async Task<ActionResult<GetCategoryRespons>> CreateBranchMeth()
        {
            var result = await _sender.SendAsync(new GetCategory());
            return Ok(result);
        }
    }
}
