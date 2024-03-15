using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.CategoriesItem.Features.GettingCategoriesItem.v1
{
    public class GetCategoryItemController : Controller
    {
        private readonly ILogger<GetCategoryItemController> _logger;
        private readonly ICommandProcessor _sender;
        private readonly CancellationToken _cancellationToken;
        public GetCategoryItemController(ILogger<GetCategoryItemController> logger, ICommandProcessor sender)
        {
            _logger = logger;
            _sender = sender;
        }
        [Route("api/category_item/get_all/v1")]
        [ApiExplorerSettings(GroupName = "category_item")]
        [HttpGet]
        public async Task<ActionResult<GetCategoryItemRespons>> CreateCategoryMeth()
        {
            var result = await _sender.SendAsync(new GetCategoryItem());
            return Ok(result);
        }
    }
}
