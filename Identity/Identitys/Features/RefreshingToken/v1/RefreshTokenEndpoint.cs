using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Identitys.Features.Login.v1;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Identity.Identitys.Features.RefreshingToken.v1;




public class RefreshTokeController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private readonly ICommandProcessor _sender;
    public RefreshTokeController(ILogger<LoginController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/identity/RefreshToke/v1")]
    [ApiExplorerSettings(GroupName = "identity")]
    [HttpPost]
    public async Task<IActionResult> RefreshTokenMeth([FromBody] RefreshTokenRequest request)
    {
        var result = await _sender.SendAsync(new RefreshToken(request.AccessToken, request.RefreshToken));
        return Ok(result);
    }
}