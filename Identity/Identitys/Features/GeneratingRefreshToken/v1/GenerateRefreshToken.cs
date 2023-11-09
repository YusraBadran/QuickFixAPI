using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuickFix.Dbcontexts;
using QuickFix.Identity.Identitys.Exceptions;
using QuickFix.Identity.Identitys.Models.DTOS.v1;
using QuickFix.Identity.Shared.Models;

namespace QuickFix.Identity.Identitys.Features.GeneratingRefreshToken.v1;
public record class GenerateRefreshToken(Guid UserId, string? Token = null) : IRequest<RefreshTokenDto>;

public class GenerateRefreshTokenHandler : IRequestHandler<GenerateRefreshToken, RefreshTokenDto>
{
    private readonly AppDbContext _context;
    public GenerateRefreshTokenHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshTokenDto> Handle(GenerateRefreshToken request, CancellationToken cancellationToken)
    {
        var refreshToken = await _context
        .Set<RefreshTokens>()
        .FirstOrDefaultAsync(rt => rt.UserId == request.UserId && rt.Token == request.Token);
        if (refreshToken == null)
        {
            var token = RefreshTokens.GetRefreshToken();
            refreshToken = new RefreshTokens
            {
                UserId = request.UserId,
                Token = token,
                CreatedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddDays(1)
            };
            await _context.Set<RefreshTokens>().AddAsync(refreshToken);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        else
        {
            if (!refreshToken.IsRefreshTokenValid())
            {
                throw new InvalidRefreshTokenException(refreshToken);
            }
            var token = RefreshTokens.GetRefreshToken();
            refreshToken.Token = token;
            refreshToken.CreatedAt = DateTime.Now;
            refreshToken.ExpiredAt = DateTime.Now.AddDays(10);
            _context.Set<RefreshTokens>().Update(refreshToken);
            await _context.SaveChangesAsync();
        }

        await RemoveOldRefreshTokens(request.UserId);
        return new RefreshTokenDto()
        {
            Token = refreshToken.Token,
            CreatedAt = refreshToken.CreatedAt,
            ExpireAt = refreshToken.ExpiredAt,
            UserId = refreshToken.UserId,
            IsActive = refreshToken.IsActive,
            IsExpired = refreshToken.IsExpired,
            IsRevoked = refreshToken.IsRevoked,
            RevokedAt = refreshToken.RevokedAt,
        };
    }

    private Task RemoveOldRefreshTokens(Guid userId, long? ttlRefreshToken = null)
    {
        var refreshTokens = _context
          .Set<RefreshTokens>()
          .Where(rt => rt.UserId == userId);

        refreshTokens.ToList().RemoveAll(rt => !rt.IsRefreshTokenValid(ttlRefreshToken));
        return _context.SaveChangesAsync();
    }

}

