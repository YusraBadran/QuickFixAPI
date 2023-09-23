using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identitys.Models;

namespace QuickFix.Identitys.Features.Register.v1;
[Route("api/[register/v1")]
[ApiController]
public class RegisterController : Controller
{
      private readonly ISender _sender;
    private readonly ILogger<RegisterController> _logger;
    public RegisterController(ILogger<RegisterController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _sender.Send(new Register(request));
        return Ok(result);
    }
}
public record class Register : RegisterRequest,IRequest<RegisterResponse>
{
    public Register(RegisterRequest request): base (request){}
}
public class RegisterHandler : IRequestHandler<Register, RegisterResponse>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<RegisterHandler> _logger;
    public RegisterHandler(UserManager<IdentityUser> userManager, ILogger<RegisterHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    public async Task<RegisterResponse> Handle(Register request, CancellationToken cancellationToken)
    {
        var user = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.phoneNumber,

        };
        var result = await _userManager.CreateAsync(user, request.password);
        if (result.Succeeded)
        {
            _logger.LogInformation("User created a new account with password.");
            return new RegisterResponse
            {
                Message = "User created a new account with password.",
                IsSuccess = true
            };
        }
        else
        {
            _logger.LogInformation("User created a new account with password.");
            return new RegisterResponse
            {
                Message = "User created a new account with password.",
                IsSuccess = true
            };
        }
    }
}