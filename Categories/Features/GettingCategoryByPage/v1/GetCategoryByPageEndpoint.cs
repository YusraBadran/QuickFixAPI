using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Categories.Features.GettingCategoryByPage.v1;

public class GetCategoryByPageController : Controller
{
    private readonly ILogger<GetCategoryByPageController> _logger;
    private readonly IQueryProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetCategoryByPageController(ILogger<GetCategoryByPageController> logger, IQueryProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/category/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "category")]
    [HttpGet]
    public async Task<ActionResult<GetCategoryByPageResponse>> GetCategoryByPageMeth(GetCategoryByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetCategoryByPage
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

    [Route("api/category/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "category")]
    [HttpPost]
    public async Task<ActionResult<GetCategoryByPageResponse>> ByPostGetCategoryByPageMeth([FromBody] GetCategoryByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetCategoryByPage
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

