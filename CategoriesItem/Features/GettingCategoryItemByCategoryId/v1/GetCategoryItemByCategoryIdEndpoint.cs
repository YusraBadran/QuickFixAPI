using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemByCategoryId.v1;

public class GetCategoryItemByCategoryIdController : Controller
{
    private readonly ILogger<GetCategoryItemByCategoryIdController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetCategoryItemByCategoryIdController(ILogger<GetCategoryItemByCategoryIdController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/category_item/get_by_category_id/v1")]
    [ApiExplorerSettings(GroupName = "category_item")]
    [HttpGet]
    public async Task<ActionResult<GetCategoryItemByCategoryIdRespons>> GetCategoryByCategoryIdMeth(Guid Id)
    {
        var result = await _sender.SendAsync(new GetCategoryItemByCategoryId(Id));
        return Ok(result);
    }
}
