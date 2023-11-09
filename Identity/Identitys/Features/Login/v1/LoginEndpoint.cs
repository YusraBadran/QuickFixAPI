using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Identitys.Models;

namespace QuickFix.Identity.Identitys.Features.Login.v1;
[Route("api/loginv/v1")]
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly ISender _sender;
    private readonly CancellationToken _cancellationToken;
    public LoginController(ILogger<LoginController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;

    }
    [HttpPost]
    public async Task<IActionResult> LoginMeth([FromBody] LoginRequest request)
    {

        var result = await _sender.Send(new Login(request));

        return Ok(result);

    }
}
