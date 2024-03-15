using Microsoft.AspNetCore.Authorization;

namespace QuickFix.Security.ApiKey.Authorization;

public class OnlyAdminsRequirement : IAuthorizationRequirement { }
