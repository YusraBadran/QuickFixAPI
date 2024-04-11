using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryPreviousBySubId.v1
{
    public class GetCategoryPreviousBySubIdController : Controller
    {
        private readonly ILogger<GetCategoryPreviousBySubIdController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetCategoryPreviousBySubIdController(ILogger<GetCategoryPreviousBySubIdController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/category/previous/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpGet]
        public async Task<ActionResult<GetCategoryPreviousBySubIdRespons>> GetCategoryPreviousBySubIdMeth(Guid Id)
        {
            var result = await _sender.SendAsync(new GetCategoryPreviousBySubId(Id));
            return Ok(result);
        }
    }
}
