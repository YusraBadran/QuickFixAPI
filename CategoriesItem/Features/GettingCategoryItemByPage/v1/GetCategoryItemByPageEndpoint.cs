using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemByPage.v1;

public class GetCategoryItemByPageController : Controller
{
    private readonly ILogger<GetCategoryItemByPageController> _logger;
    private readonly IQueryProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetCategoryItemByPageController(ILogger<GetCategoryItemByPageController> logger, IQueryProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/category_item/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "category_item")]
    [HttpGet]
    public async Task<ActionResult<GetCategoryItemByPageResponse>> GetCategoryByPageMeth(GetCategoryItemByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetCategoryItemByPage
            {
                Filters = request.Filters,
                Includes = request.Includes,
                Page = request.Page,
                Sorts = request.Sorts,
                PageSize = request.PageSize
            },
            _cancellationToken
        );
        return Ok(result);
    }

    [Route("api/category_item/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "category_item")]
    [HttpPost]
    public async Task<ActionResult<GetCategoryItemByPageResponse>> ByPostGetCategoryByPageMeth([FromBody] GetCategoryItemByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetCategoryItemByPage
            {
                Filters = request.Filters,
                Includes = request.Includes,
                Page = request.Page,
                Sorts = request.Sorts,
                PageSize = request.PageSize
            },
            _cancellationToken
        );
        return Ok(result);
    }
}

