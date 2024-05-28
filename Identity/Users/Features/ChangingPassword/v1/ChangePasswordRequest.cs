namespace QuickFix.Identity.Users.Features.ChangingPassword.v1;

public record ChangePasswordRequest
{
    public string UserNameOrEmail { get; init; }
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }
    public string ConfirmPassword { get; init; }

}
