using QuickFix.Identity.Identitys.Features.GettingClaims.v1;
using QuickFix.Identity.Identitys.Models;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Identity.Identitys.Features.GettingClaims.v1;


public class GetClaimsController : Controller
{
    private readonly ILogger<GetClaimsController> _logger;
    private readonly ISender _sender;


    public GetClaimsController(ILogger<GetClaimsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
[Route("/api/identity/getclaims/v1")]
[ApiExplorerSettings(GroupName = "identity")]
    [HttpGet]
    public async Task<IActionResult> GetClaimsMeth()
    {
        var result = await _sender.Send(new GetClaims());

        return Ok(result);  
    }
    
}