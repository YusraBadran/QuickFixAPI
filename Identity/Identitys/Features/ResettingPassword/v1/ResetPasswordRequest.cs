namespace QuickFix.Identity.Identitys.Features.ResettingPassword.v1;

public record ResetPasswordRequest
{
    public string UserNameOrEmail { get; init; }
    public string NewPassword { get; init; }
    public string ConfirmPassword { get; init; }

}
