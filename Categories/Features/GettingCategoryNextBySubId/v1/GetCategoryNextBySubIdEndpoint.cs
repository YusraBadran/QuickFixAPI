using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryNextBySubId.v1
{
    public class GetCategoryNextByIdController : Controller
    {
        private readonly ILogger<GetCategoryNextByIdController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetCategoryNextByIdController(ILogger<GetCategoryNextByIdController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/category/get_by_id/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpGet]
        public async Task<ActionResult<GetCategoryNextBySubIdRespons>> GetCategoryByIdMeth(Guid Id)
        {
            var result = await _sender.SendAsync(new GetCategoryNextBySubId(Id));
            return Ok(result);
        }
    }
}
