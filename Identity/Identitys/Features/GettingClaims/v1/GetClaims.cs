using MediatR;
using NuGet.Protocol.Plugins;

namespace QuickFix.Identity.Identitys.Features.GettingClaims.v1;

public record GetClaims : IRequest<GetClaimsResponse> { }

public class GetClaimsyHandler : IRequestHandler<GetClaims, GetClaimsResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetClaimsyHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<GetClaimsResponse> Handle(GetClaims request, CancellationToken cancellationToken)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims.Select(
            x => new ClaimDot { Type = x.Type, Value = x.Value }
        );

        return Task.FromResult(new GetClaimsResponse(claims));
    }
}