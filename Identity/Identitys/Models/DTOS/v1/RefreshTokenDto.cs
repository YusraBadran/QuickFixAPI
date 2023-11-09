using System;

namespace QuickFix.Identity.Identitys.Models.DTOS.v1
{
    public record class RefreshTokenDto
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
    public bool IsExpired { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsActive { get; set; }
    public DateTime? RevokedAt { get; set; }
}
}
