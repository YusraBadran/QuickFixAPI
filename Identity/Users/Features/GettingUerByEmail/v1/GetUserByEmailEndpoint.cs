using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Identitys.Features.Login.v1;
using QuickFix.Identity.Identitys.Features.RefreshingToken.v1;

namespace QuickFix.Identity.Users.Features.GettingUerByEmail.v1;

[Route("api/GettingUerByEmail/v1")]

public class GettingUerByEmailController:Controller
{
    
    private readonly ILogger<GettingUerByEmailController> _logger;

    private readonly ISender _sender;

    public GettingUerByEmailController(ILogger<GettingUerByEmailController>logger , ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GettingUerByEmailMeth(string request)
    {
        var response = await _sender.Send();
        return Ok(response);
    }
}


