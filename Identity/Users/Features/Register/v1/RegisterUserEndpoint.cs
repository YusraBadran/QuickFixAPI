using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Users.Models.RegisterUser;

namespace QuickFix.Identity.Users.Features.Register.v1;
[Route("api/register/v1")]
public class RegisterController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<RegisterController> _logger;
    private readonly CancellationToken _cancellationToken;
    public RegisterController(IMediator mediator, ILogger<RegisterController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> RegisterMeth([FromBody] RegisterUserRequest request )
    {
        var respons = await _mediator.Send(request,_cancellationToken);
        return Ok(respons);
    }
    // private static async Task<IResult> RegisterUser(
    //       RegisterUserRequest request,
    //       IMediator commandProcessor,
    //       CancellationToken cancellationToken
    //   )
    // {
    //     var command = new RegisterUser(
    //         request.FirstName,
    //         request.LastName,
    //         request.UserName,
    //         request.Email,
    //         request.PhoneNumber,
    //         request.Password,
    //         request.ConfirmPassword,
    //         request.Roles?.ToList()
    //     );

    //     var result = await commandProcessor.Send(command, cancellationToken);
    //     return (IResult)request;
    // } 
}
