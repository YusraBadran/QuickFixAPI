using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.DeletCategories.v1;

public class DeleteCategoryController : Controller
{
    private readonly ICommandProcessor _sender;
    private readonly ILogger<DeleteCategoryController> _logger;
    private readonly CancellationToken _cancellationToken;
    public DeleteCategoryController(ICommandProcessor sender, ILogger<DeleteCategoryController> logger)
    {
        _sender = sender;
        _logger = logger;
        //_cancellationToken = new CancellationToken();
    }
    [Route("api/category/delete/v1")]
    [ApiExplorerSettings(GroupName = "category")]
    [HttpDelete]
    public async Task<IActionResult> DleteCategoryMath(Guid Id)
    {

        var respons = await _sender.SendAsync(new DeleteCategory(Id));
        return Ok(respons);
    }
