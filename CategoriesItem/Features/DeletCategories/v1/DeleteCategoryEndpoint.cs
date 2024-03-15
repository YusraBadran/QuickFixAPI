using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.CategoriesItem.Features.DeletCategories.v1;

public class DeleteCategoryItemController : Controller
{
    private readonly ICommandProcessor _sender;
    private readonly ILogger<DeleteCategoryItemController> _logger;
    private readonly CancellationToken _cancellationToken;
    public DeleteCategoryItemController(ICommandProcessor sender, ILogger<DeleteCategoryItemController> logger)
    {
        _sender = sender;
        _logger = logger;
        //_cancellationToken = new CancellationToken();
    }
    [Route("api/category_item/delete/v1")]
    [ApiExplorerSettings(GroupName = "category_item")]
    [HttpDelete]
    public async Task<IActionResult> DleteCategoryMath(Guid Id)
    {

        var respons = await _sender.SendAsync(new DeleteCategoryItem(Id));
        return Ok(respons);
    }
    [Route("api/category_item/delete/v1")]
    [ApiExplorerSettings(GroupName = "category_item")]
    [HttpPost]
    public async Task<IActionResult> PostDleteCategoryMath(Guid Id)
    {

        var respons = await _sender.SendAsync(new DeleteCategoryItem(Id));
        return Ok(respons);
    }
}
