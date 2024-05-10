using QuickFix.Security.ApiKey.Authorization;
using QuickFix.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.OrdersService.Features.GettingOrdersByPage.v1;

public class GetOrderByPageController : Controller
{
    private readonly ILogger<GetOrderByPageController> _logger;
    private readonly IQueryProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public GetOrderByPageController(ILogger<GetOrderByPageController> logger, IQueryProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    [Route("api/orders/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "orders")]
    [HttpGet]
    public async Task<ActionResult<GetOrderByPageRespons>> GetOrderByPageMeth( GetOrderByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetOrderByPage()
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin,user")]
    [Route("api/orders/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "orders")]
    [HttpPost]
    public async Task<ActionResult<GetOrderByPageRespons>> ByPostGetOrderByPageMeth( [FromBody] GetOrderByPageRequest request)
    {
        var result = await _sender.SendAsync(
            new GetOrderByPage()
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