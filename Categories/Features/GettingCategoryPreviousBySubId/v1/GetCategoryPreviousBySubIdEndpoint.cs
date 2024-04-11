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
        [Route("api/category/get_by_id/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpGet]
        public async Task<ActionResult<GetCategoryPreviousBySubIdRespons>> GetCategoryByIdMeth(Guid Id)
        {
            var result = await _sender.SendAsync(new GetCategoryPreviousBySubId(Id));
            return Ok(result);
        }
    }
}
