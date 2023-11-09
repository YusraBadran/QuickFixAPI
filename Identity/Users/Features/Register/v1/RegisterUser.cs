using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Exceptions;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Identity.Users.Models.RegisterUser;
using IdentityConstants = QuickFix.Identity.Shared.Models.IdentityConstants;
using UserState = QuickFix.Identity.Shared.Models.UserState;

namespace QuickFix.Users.Features.Register.v1;




    public record class RegisterUser : RegisterUserRequest, IRequest<RegisterUserResponse>
{
    public DateTime CreatedAt { get; init; }

    public RegisterUser(RegisterUserRequest request) : base(request)
    {
        CreatedAt = DateTime.UtcNow;
    }
}
    public class RegisterUserValidator:AbstractValidator<RegisterUser>
    {
    
        public RegisterUserValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(v=>v.FirstName).NotEmpty().NotNull().WithMessage("FirstName is required.");
            RuleFor(v=>v.LastName).NotEmpty().NotNull().WithMessage("LastName is required.");
            RuleFor(v=>v.UserName).NotEmpty().NotNull().WithMessage("UserName is required.");
            RuleFor(v=>v.Email).NotEmpty().NotNull().WithMessage("Email is required.").EmailAddress();
            RuleFor(v=>v.PhoneNumber).NotEmpty().NotNull()
            .WithMessage("PhoneNumber is required.")
            .MinimumLength(7).WithMessage("PhoneNumber must not be less than 7 numbers.")
            .MaximumLength(15).WithMessage("PhoneNumber must not be more than 15 numbers.");
            RuleFor(v=>v.ConfirmPassword)
            .Equal(v=>v.Password)
            .WithMessage("Password and ConfirmPassword must be equal.").NotEmpty().NotNull();
            RuleFor(v=>v.Roles)
            .Custom(
                (roles,c)=>
                {
                    if(roles != null&&!roles.All(
                        x=> x.Contains(IdentityConstants.Role.Admin, StringComparison.Ordinal)||
                        x.Contains(IdentityConstants.Role.User, StringComparison.Ordinal)
                    )){
                        c.AddFailure("Invalid role.");
                    }
                }
            );
        }
    }
/**
* start handler
*/
public class RegisterHandler : IRequestHandler<RegisterUser, RegisterUserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<RegisterHandler> _logger;
    public RegisterHandler(UserManager<ApplicationUser> userManager, ILogger<RegisterHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
           var applicationUser = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserState = UserState.Active,
            CreatedAt = request.CreatedAt,
        };
          var identityResult = await _userManager.CreateAsync(applicationUser, request.Password);
        if (!identityResult.Succeeded)
            throw new RegisterIdentityUserException(string.Join(',', identityResult.Errors.Select(e => e.Description)));

        var roleResult = await _userManager.AddToRolesAsync(
            applicationUser,
            request.Roles ?? new List<string> { IdentityConstants.Role.User }
        );

        if (!roleResult.Succeeded)
            throw new RegisterIdentityUserException(string.Join(',', roleResult.Errors.Select(e => e.Description)));

             return new RegisterUserResponse()
            
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber,
                UserName = applicationUser.UserName,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Roles = request.Roles ?? new List<string> { IdentityConstants.Role.User },
                RefreshTokens = applicationUser?.RefreshTokens?.Select(x => x.Token),
                CreatedAt = request.CreatedAt,
                UserState = UserState.Active
            };
        
        
    }
}