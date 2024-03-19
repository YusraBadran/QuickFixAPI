using Ardalis.GuardClauses;

using QuickFix.Identity.Identitys.Features.RefreshingToken.v1;
using QuickFix.Identity.Identitys.Models.DTOS.v1;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickFix.DbContexts;

namespace QuickFix.Identity.Identitys.Features.GeneratingRefreshToken.v1;
public record GenerateRefreshToken(Guid UserId, string? Token = null) : ICommand<GenerateRefreshTokenResponse>;

public class GenerateRefreshTokenHandler : ICommandHandler<GenerateRefreshToken, GenerateRefreshTokenResponse>
{
    private readonly AppDbContext _context;

    public GenerateRefreshTokenHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GenerateRefreshTokenResponse> Handle(
        GenerateRefreshToken request,
        CancellationToken cancellationToken
    )
    {
        Guard.Against.Null(request, nameof(GenerateRefreshToken));

        var refreshToken = await _context
            .Set<Shared.Models.RefreshTokens>()
            .FirstOrDefaultAsync(rt => rt.UserId == request.UserId && rt.Token == request.Token);

        if (refreshToken == null)
        {
            var token = Shared.Models.RefreshTokens.GetRefreshToken();

            refreshToken = new Shared.Models.RefreshTokens
            {
                UserId = request.UserId,
                Token = token,
                CreatedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddDays(1),

            };

            await _context.Set<Shared.Models.RefreshTokens>().AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }
        else
        {
            if (!refreshToken.IsRefreshTokenValid())
                throw new InvalidRefreshTokenException(refreshToken);

            var token = Shared.Models.RefreshTokens.GetRefreshToken();

            refreshToken.Token = token;
            refreshToken.ExpiredAt = DateTime.Now;
            refreshToken.CreatedAt = DateTime.Now.AddDays(10);


            _context.Set<Shared.Models.RefreshTokens>().Update(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // remove old refresh tokens from user
        // we could also maintain them on the database with changing their revoke date
        await RemoveOldRefreshTokens(request.UserId);

        return new GenerateRefreshTokenResponse(
            new RefreshTokenDto
            {
                Token = refreshToken.Token,
                CreatedAt = refreshToken.CreatedAt,
                ExpireAt = refreshToken.ExpiredAt,
                UserId = refreshToken.UserId,
                IsActive = refreshToken.IsActive,
                IsExpired = refreshToken.IsExpired,
                IsRevoked = refreshToken.IsRevoked,
                RevokedAt = refreshToken.RevokedAt
            }
        );
    }

    private Task RemoveOldRefreshTokens(Guid userId, long? ttlRefreshToken = null)
    {
        var refreshTokens = _context
            .Set<Shared.Models.RefreshTokens>()
            .Where(rt => rt.UserId == userId);

        refreshTokens.ToList().RemoveAll(x => !x.IsRefreshTokenValid(ttlRefreshToken));

        return _context.SaveChangesAsync();
    }
}
