using AutoMapper;
using QuickFix.OrdersService.Models.DTOs;
using QuickFix.Security.Jwt;
using QuickFix.Settings.Notifications.Models;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using QuickFix.OrdersService.Data;
using QuickFix.OrdersService.Extensions;
using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Shared.Models;
using IdentityConstants = QuickFix.Identity.Shared.Models.IdentityConstants;

namespace QuickFix.OrdersService.Features.NotificationsOrder.v1;

public record NotificationOrder(Guid? Id) : ICommand<NotificationOrderRespons>
{
}

public class LookUpCompanyHandler : ICommandHandler<NotificationOrder, NotificationOrderRespons>
{
    private readonly IMapper _mapper;
    private readonly IOrdersServiceDbContext _orderContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ISecurityContextAccessor _security;

    public LookUpCompanyHandler(IMapper mapper, IOrdersServiceDbContext orderContext, ISecurityContextAccessor security, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _orderContext = orderContext;
        _security = security;
        _userManager = userManager;
    }
    public async Task<NotificationOrderRespons> Handle(NotificationOrder request, CancellationToken cancellationToken)
    {
        var userId = _security.UserId;
        var IsuserClient = await _userManager.FindByIdAsync(userId);
        if (IsuserClient != null)
        {
            var roles = await _userManager.IsInRoleAsync(IsuserClient, IdentityConstants.Role.User);
            if (roles)
            {
                var dataDto = await _orderContext.NotificationsUserOrder((Guid)request.Id);
                var resultDto = _mapper.Map<List<NotificationRequest>>(dataDto);
                return new NotificationOrderRespons(resultDto);
            }
        }

        var data = await _orderContext.NotificationsOrder();
        var result = _mapper.Map<List<NotificationRequest>>(data);
        return new NotificationOrderRespons(result);
    }
}
