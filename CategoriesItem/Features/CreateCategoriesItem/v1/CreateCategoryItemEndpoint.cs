using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.CategoriesItem.Features.CreateCategoriesItem.v1
{
    public class CreateCategoryItemController : Controller
    {
        private readonly ILogger<CreateCategoryItemController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public CreateCategoryItemController(ILogger<CreateCategoryItemController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;

        }

        [Route("api/category_item/create/v1")]
        [ApiExplorerSettings(GroupName = "category_item")]
        [HttpPost]
        public async Task<ActionResult<CreateCategoryItemRequest>> CreateCategoryItem([FromBody] 
        CreateCategoryItemRequest request)
        {

            var result = await _sender.SendAsync(new CreateCategoryItem(request));
            return Ok(result);
        }
    }
}
