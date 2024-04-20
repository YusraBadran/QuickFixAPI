using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryBySubId.v1
{
    public class GetCategoryBySubIdController : Controller
    {
        private readonly ILogger<GetCategoryBySubIdController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetCategoryBySubIdController(ILogger<GetCategoryBySubIdController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/category/sub/get_by_id/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpGet]
        public async Task<ActionResult<GetCategoryBySubIdRespons>> GetCategoryBySubIdMeth(Guid Id)
        {
            var result = await _sender.SendAsync(new GetCategoryBySubId(Id));
            return Ok(result);
        }
    }
}
