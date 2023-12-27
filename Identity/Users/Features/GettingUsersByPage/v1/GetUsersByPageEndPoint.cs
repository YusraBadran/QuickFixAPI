using System;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Users.Features.GettingUsersByPage.v1;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Identity.Users.Features.GettingAllUserByPage.v1;

public class GetUsersByPageController : Controller
{
    private readonly ILogger<GetUsersByPageController> _logger;

    private readonly CancellationToken _cancellationToken;
    private readonly IQueryProcessor _sender;

    public GetUsersByPageController(ILogger<GetUsersByPageController> logger, IQueryProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }

    //for App
    [Route("api/user/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "user")]
    [HttpGet]
    public async Task<IActionResult> GetUsersByPageMeth([FromQuery] GetUsersByPageRequest? request)
    {
        var response = await _sender.SendAsync(new GetUsersByPage{
            Filters = request.Filters,
            Includes = request.Includes,
            Page = request.Page,
            Sorts = request.Sorts,
            PageSize = request.PageSize
            },_cancellationToken);
        return Ok(response);
    }


    //for web
    [Route("api/user/get_by_page/v1")]
    [ApiExplorerSettings(GroupName = "user")]
    [HttpPost]
    public async Task<IActionResult> ByPostGetUsersByPageMeth([FromBody] GetUsersByPageRequest? request)
    {
        var response = await _sender.SendAsync(new GetUsersByPage{
            Filters = request.Filters,
            Includes = request.Includes,
            Page = request.Page,
            Sorts = request.Sorts,
            PageSize = request.PageSize
            },_cancellationToken);
        return Ok(response);
    }
}