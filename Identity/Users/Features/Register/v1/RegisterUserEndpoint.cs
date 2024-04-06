using System;
using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Users.Features.Register.v1;

namespace QuickFix.Identity.Users.Features.Register.v1;

public class RegisterController : Controller
{
    private readonly ICommandProcessor _sender;
    private readonly ILogger<RegisterController> _logger;

    public RegisterController(ICommandProcessor sender, ILogger<RegisterController> logger)
    {
        _sender = sender;
        _logger = logger;
    }
    [Route("api/user/register/v1")]
    [ApiExplorerSettings(GroupName = "user")]
    [HttpPost]
    public async Task<ActionResult<RegisterUsersRequest>> RegisterMeth([FromBody] RegisterUsersRequest request)
    {
        var dtos = new RegisterUserRequest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserState = request.UserState,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
            Roles = request.Roles,
            Permissions = null,

        };
        ///<summary>
        /// انشاء مستخدم جديد
        /// </summary>
        var result = await _sender.SendAsync(new RegisterUser(dtos));

        return Ok(result);
    }
    [Route("api/user/create/v1")]
    [ApiExplorerSettings(GroupName = "user")]
    [HttpPost]
    public async Task<ActionResult<RegisterUsersRequest>> CreateMeth([FromBody] RegisterUserRequest request)
    {
        ///<summary>
        /// انشاء مستخدم جديد
        /// </summary>
        var result = await _sender.SendAsync(new RegisterUser(request));

        return Ok(result);
    }

}
