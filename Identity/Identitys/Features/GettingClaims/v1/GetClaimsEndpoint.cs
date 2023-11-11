using QuickFix.Identity.Identitys.Features.GettingClaims.v1;
using QuickFix.Identity.Identitys.Models;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Identity.Identitys.Features.GettingClaims.v1;
[Route("/api/getclaims/v1")]

public class GetClaimsController : Controller
{
    private readonly ILogger<GetClaimsController> _logger;
    private readonly ISender _sender;
    private readonly CancellationToken _cancellationToken;

    public GetClaimsController(ILogger<GetClaimsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetClaimsMeth()
    {
        var result = await _sender.Send(new GetClaims());

        return Ok(result);  
    }
    
}