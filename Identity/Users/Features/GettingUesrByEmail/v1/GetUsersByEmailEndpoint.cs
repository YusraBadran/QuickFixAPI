using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Identitys.Features.Login.v1;
using QuickFix.Identity.Identitys.Features.RefreshingToken.v1;
using QuickFix.Shared.Abstractions.Queries;

namespace QuickFix.Identity.Users.Features.GettingUserByEmail.v1;

public class GetUserByEmailController : Controller
{

    private readonly ILogger<GetUserByEmailController> _logger;
    private readonly CancellationToken _cancellationToken;
    private readonly IQueryProcessor _sender;
    public GetUserByEmailController(IQueryProcessor sender, ILogger<GetUserByEmailController> logger)
    {
        _sender = sender;
        _logger = logger;

    }
    [Route("api/user/get_by_email/v1")]
    [ApiExplorerSettings(GroupName = "user")]
    [HttpGet]
    public async Task<ActionResult> GetUserByEmailMeth(string email)
    {
        var result = await _sender.SendAsync(new GetUserByEmail(email), _cancellationToken);
        return Ok(result);
    }
}


