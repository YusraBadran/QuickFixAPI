using System;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Identity.Users.Features.DeleteUsers.v1;

public class DeleteUserController : Controller
{
    private readonly ILogger<DeleteUserController> _logger;

    private readonly ICommandProcessor _sender;

    public DeleteUserController(ILogger<DeleteUserController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;

    }

    [Route("api/user/delete/v1")]
    [ApiExplorerSettings(GroupName = "user")]
    [HttpDelete]
    public async Task<ActionResult<DeleteUserRequest>> DeleteUserMeth(Guid Id)
    {
        var respons = await _sender.SendAsync(new DeleteUser(Id));
        return Ok(respons);
    }



}

