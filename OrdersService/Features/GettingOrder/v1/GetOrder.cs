using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using QuickFix.Security.Jwt;
using QuickFix.Identity.Shared.Models;
using Microsoft.AspNetCore.Identity;
using QuickFix.OrdersService.Data;
using QuickFix.OrdersService.Extensions;
using QuickFix.OrdersService.Models.DTOs;
using IdentityConstants = QuickFix.Identity.Shared.Models.IdentityConstants;

namespace QuickFix.OrdersService.Features.GettingOrder.v1;

public record GetOrder : ITxCommand<GetOrderRespons>
{
}
public class GetOrderHandler : ICommandHandler<GetOrder, GetOrderRespons>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOrdersServiceDbContext _orderContexct;
    private readonly ISecurityContextAccessor _security;
    public GetOrderHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IOrdersServiceDbContext orderContext, ISecurityContextAccessor security)
    {
        _mapper = mapper;
        _orderContexct = orderContext;
        _security = security;
        _userManager = userManager;

    }
    public async Task<GetOrderRespons> Handle(GetOrder request, CancellationToken cancellationToken)
    {
        var userId = _security.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            var IsuserClient = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.IsInRoleAsync(IsuserClient, IdentityConstants.Role.User);
            if (roles)
            {
                var orderUser = await _orderContexct.FindAllUserOrders(Guid.Parse(userId));
                var orderDtoUser = _mapper.Map<IEnumerable<OrdersDto>>(orderUser);
                return new GetOrderRespons(orderDtoUser);
            }
        }
        var orderAdmin = await _orderContexct.FindAllOrders();
        var orderDtoAdmin = _mapper.Map<IEnumerable<OrdersDto>>(orderAdmin);
        return new GetOrderRespons(orderDtoAdmin);
    }
}
