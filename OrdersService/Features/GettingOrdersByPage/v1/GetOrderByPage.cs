using AutoMapper;

using QuickFix.Identity.Shared.Models;
using QuickFix.OrdersService.Features.GettingOrder.v1;
using QuickFix.Security.Jwt;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;
using Microsoft.AspNetCore.Identity;
using QuickFix.OrdersService.Data;
using QuickFix.OrdersService.Extensions;
using QuickFix.OrdersService.Features.GettingOrdersByPage.v1;
using QuickFix.OrdersService.Models.DTOs;
using IdentityConstants = QuickFix.Identity.Shared.Models.IdentityConstants;

namespace QuickFix.OrdersService.Features.GettingOrdersByPage.v1
{
    public record GetOrderByPage(Guid? Id) : ListQuery<GetOrderByPageRespons>
    {
    }
}
public class GetOrderByPageHandler : IQueryHandler<GetOrderByPage, GetOrderByPageRespons>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOrdersServiceDbContext _orderContexct;
    private readonly ISecurityContextAccessor _security;
    public GetOrderByPageHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IOrdersServiceDbContext orderContext, ISecurityContextAccessor security)
    {
        _mapper = mapper;
        _orderContexct = orderContext;
        _security = security;
        _userManager = userManager;
    }

    public async Task<GetOrderByPageRespons> Handle(GetOrderByPage request, CancellationToken cancellationToken)
    {
        var userId = _security.UserId;
        var IsuserClient = await _userManager.FindByIdAsync(request.Id.ToString());
        if (IsuserClient != null)
        {
            var roles = await _userManager.IsInRoleAsync(IsuserClient, IdentityConstants.Role.User);
            if (roles)
            {
                var orderUser = await _orderContexct.FindAllUserOrdersByPageAsync<OrdersDto>(IsuserClient.Id, _mapper, request, cancellationToken);
                return new GetOrderByPageRespons(orderUser);
            }
        }
        var orderDTOs = await _orderContexct.FindAllOrdersByPageAsync<OrdersDto>(_mapper, request, cancellationToken);
        return new GetOrderByPageRespons(orderDTOs);
    }
}
