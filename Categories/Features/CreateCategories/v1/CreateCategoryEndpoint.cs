using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.CreateCategories.v1
{
    public class CreateCategoryController : Controller
    {
        private readonly ILogger<CreateCategoryController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public CreateCategoryController(ILogger<CreateCategoryController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
            //_cancellationToken = cancellationToken;
        }

        [Route("api/category/create/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpPost]
        public async Task<ActionResult<CreateCategoryRequest>> CreateCategoryMeth([FromBody] CreateCategoryRequest request)
        {

            var result = await _sender.SendAsync(new CreateCategory(request));
            return Ok(result);
        }
    }
}
