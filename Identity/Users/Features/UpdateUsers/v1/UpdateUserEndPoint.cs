using System;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Identity.Users.Features.UpdateUsers.v1;
public class UpdateUserController : Controller
{
    private readonly ILogger<UpdateUserController> _logger;

    private readonly CancellationToken _cancellationToken;
    private readonly ICommandProcessor _sender;

    public UpdateUserController(ILogger<UpdateUserController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [Route("api/user/update/v1")]
    [ApiExplorerSettings(GroupName = "user")]
    [HttpPut]
   public async Task<ActionResult<UpdateUserRequest>> UpdateUserMeth([FromBody] UpdateUserRequest request)
    {
        var respons = await _sender.SendAsync(new UpdateUser(request));
        return Ok(respons);
    }

}
