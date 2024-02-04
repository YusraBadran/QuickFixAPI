using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Categories.Features.GettingCategoryByServiceTypeIdByPage.v1;

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
    [Route("api/category_service_type_id/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "category")]
    [HttpGet]
    public async Task<ActionResult<GetCategoryByServiceTypeIdByPageResponse>> GetCategoryByServiceTypeIdByPageMeth(Guid Id, GetCategoryByServiceTypeIdByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetCategoryByServiceTypeIdByPage(Id)
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

    [Route("api/category_service_type_id/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "category")]
    [HttpPost]
    public async Task<ActionResult<GetCategoryByServiceTypeIdByPageResponse>> ByPostGetCategoryByServiceTypeIdByPageMeth(Guid Id, [FromBody] GetCategoryByServiceTypeIdByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetCategoryByServiceTypeIdByPage(Id)
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

