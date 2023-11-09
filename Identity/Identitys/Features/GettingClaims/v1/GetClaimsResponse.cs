using System;

namespace QuickFix.Identity.Identitys.Features.GettingClaims.v1;

public record GetClaimsResponse(IEnumerable<ClaimDot> Claims);
