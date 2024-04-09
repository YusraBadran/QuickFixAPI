using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.MaintenanceCenters.Features.GettingCentersByPage.v1;

public class GetCenterByPageController : Controller
{
    private readonly ILogger<GetCenterByPageController> _logger;
    private readonly IQueryProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetCenterByPageController(ILogger<GetCenterByPageController> logger, IQueryProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/centers/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "centers")]
    [HttpGet]
    public async Task<ActionResult<GetCenterByPageResponse>> GetCenterByPageMeth(GetCenterByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetCenterByPage
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

    [Route("api/centers/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "centers")]
    [HttpPost]
    public async Task<ActionResult<GetCenterByPageResponse>> ByPostGetCenterByPageMeth([FromBody] GetCenterByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetCenterByPage
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

