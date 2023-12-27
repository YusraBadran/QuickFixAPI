using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Identitys.Features.Login.v1;
using QuickFix.Identity.Identitys.Models;

namespace QuickFix.Identity.Identitys.Features.RefreshingToken.v1;




public class RefreshTokeController:Controller
{
    private readonly ILogger<LoginController> _logger;

    private readonly ISender _sender;
    public RefreshTokeController(ILogger<LoginController>logger,ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
[Route("api/identity/RefreshToke/v1")]
[ApiExplorerSettings(GroupName = "identity")]
    [HttpPost]
    public async Task<IActionResult> RefreshTokenMeth([FromBody] RefreshTokenRequest request)
    {
        var result = await _sender.Send(new RefreshToken(request));
        return Ok(result);
    }
}