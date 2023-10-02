using System;
using System.ComponentModel.DataAnnotations;

namespace QuickFix.Identity.Shared.Models
{
    public class AccessToken
    {
    [Key]
    public Guid UserId { get; set; }
    public string Token { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiredAt { get; set; }
    public string CreatedByIp { get; set; } = default!;
    public ApplicationUser? ApplicationUser { get; set; }
    }
}
