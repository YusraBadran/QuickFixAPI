using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryById.v1
{
    public class GetCategoryByIdController : Controller
    {
        private readonly ILogger<GetCategoryByIdController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetCategoryByIdController(ILogger<GetCategoryByIdController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/category/get_by_id/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpGet]
        public async Task<ActionResult<GetCategoryByIdRespons>> GetServiceTypeByIdMeth(Guid Id)
        {
            var result = await _sender.SendAsync(new GetCategoryById(Id));
            return Ok(result);
        }
    }
}
