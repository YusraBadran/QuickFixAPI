using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemById.v1;

public class GetCategoryItemByIdController : Controller
{
    private readonly ILogger<GetCategoryItemByIdController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetCategoryItemByIdController(ILogger<GetCategoryItemByIdController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/category_item/get_by_id/v1")]
    [ApiExplorerSettings(GroupName = "category_item")]
    [HttpGet]
    public async Task<ActionResult<GetCategoryItemByIdRespons>> GetCategoryByIdMeth(Guid Id)
    {
        var result = await _sender.SendAsync(new GetCategoryItemById(Id));
        return Ok(result);
    }
}
