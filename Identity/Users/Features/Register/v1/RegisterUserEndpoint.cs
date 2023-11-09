using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Users.Models.RegisterUser;
using QuickFix.Users.Features.Register.v1;

namespace QuickFix.Identity.Users.Features.Register.v1;
[Route("api/register/v1")]
public class RegisterController : Controller
{
    private readonly ISender _sender;
    private readonly ILogger<RegisterController> _logger;
   
    public RegisterController(ISender sender, ILogger<RegisterController> logger)
    {
        _sender = sender;
        _logger = logger;
    }
    [HttpPost]
   public async Task<IActionResult> RegisterMeth([FromBody] RegisterUserRequest request)
    {

        var respons = await _sender.Send(new RegisterUser(request));
        return Ok(respons);
    }

}
