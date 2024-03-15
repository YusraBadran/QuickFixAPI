using Microsoft.AspNetCore.Mvc;

using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.CategoriesItem.Features.UpdateCategoryItem.v1;

public class UpdateCategoryItemController : Controller
{
    private readonly ILogger<UpdateCategoryItemController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public UpdateCategoryItemController(ILogger<UpdateCategoryItemController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
        //_cancellationToken = cancellationToken;
    }

    [Route("api/category_item/update/v1")]
    [ApiExplorerSettings(GroupName = "category_item")]
    [HttpPut]
    public async Task<ActionResult<UpdateCategoryItemRequest>> UpdateCategoryMeth([FromBody] UpdateCategoryItemRequest request)
    {

        var result = await _sender.SendAsync(new UpdateCategoryItem(request));
        return Ok(result);
    }
}
