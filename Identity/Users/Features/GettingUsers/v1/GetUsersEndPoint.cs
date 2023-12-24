using System;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Identity.Users.Features.GettingUsers.v1;

    public class GetUsersController:Controller
    {
    private readonly ILogger<GetUsersController> _logger;

    private readonly CancellationToken _cancellationToken;
    private readonly ICommandProcessor _sender;

    public GetUsersController(ILogger<GetUsersController> logger,ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [Route("api/user/get_all/v1")]
    [HttpGet]
    public async Task<IActionResult> GetUsersMeth()
    {
        var response = await _sender.SendAsync(new GetUsers());
        return Ok(response);
    }
}
