using Microsoft.AspNetCore.Mvc;
using QuickFix.Categories.Features.UpdateCategories.v1;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.CreateCategories.v1
{
    public class UpdateCategoryController : Controller
    {
        private readonly ILogger<UpdateCategoryController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public UpdateCategoryController(ILogger<UpdateCategoryController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
            //_cancellationToken = cancellationToken;
        }

        [Route("api/category/update/v1")]
        [ApiExplorerSettings(GroupName = "category")]
        [HttpPut]
        public async Task<ActionResult<UpdateCategoryRequest>> UpdateCategoryMeth([FromBody] UpdateCategoryRequest request)
        {

            var result = await _sender.SendAsync(new UpdateCategory(request));
            return Ok(result);
        }
    }
}
