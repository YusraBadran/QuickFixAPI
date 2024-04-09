using AutoMapper;
using QuickFix.Security.Jwt;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Settings.Menus.Model;
using QuickFix.Settings.Screens.Data;
using QuickFix.Settings.Screens.Extensions;
using QuickFix.Settings.Screens.Model;

namespace QuickFix.Settings.Menus.Features.GettingMenu.v1;

public record GetMenu(Guid? Id) : ICommand<GetMenuRespons>
{
}
public class GetMenuHandler : ICommandHandler<GetMenu, GetMenuRespons>
{
    private readonly ILogger<GetMenuHandler> _logger;
    private readonly IScreenContext _menuContext;
    private readonly IMapper _mapper;
    private readonly ISecurityContextAccessor _security;
    List<IHasMenuItems> applications = new List<IHasMenuItems>();
    public GetMenuHandler(ILogger<GetMenuHandler> logger, IScreenContext menuContext, IMapper mapper, ISecurityContextAccessor security)
    {
        _logger = logger;
        _menuContext = menuContext;
        _mapper = mapper;
        _security = security;
    }
    public async Task<GetMenuRespons> Handle(GetMenu request, CancellationToken cancellationToken = default)
    {

        var userId = _security.UserId;
        if (userId == null)
        {
            userId = request.Id.ToString();
        }
        var result = await _menuContext.GetUserMenu(Guid.Parse(userId));
        var appMenu = new ApplicationMenuItem();

        if (result != null || result.Count() > 0)
        {

            foreach (var menuItemDto in result.Where(x => x.SubId == null))
            {
                AddChildItems(menuItemDto, result, appMenu);

            }

        }
        return new GetMenuRespons(appMenu.Items ?? new List<ApplicationMenuItem>());
    }


    private void AddChildItems(ScreenModel menuItem, List<ScreenModel> source, IHasMenuItems? parent = null)
    {
        var applicationMenuItem = CreateApplicationMenuItem(menuItem);

        foreach (var item in source.Where(x => x.SubId == menuItem.Id))
        {
            AddChildItems(item, source, applicationMenuItem);
        }

        parent?.Items.Add(applicationMenuItem);

    }

    private ApplicationMenuItem CreateApplicationMenuItem(ScreenModel menuItem)
    {
        var routerLink = menuItem.RouterLink?.Split(',').Select(x => x.Trim());
        return new ApplicationMenuItem()
        {
            Id = menuItem.Id,
            Label = menuItem.Label,
            Icon = menuItem.Icon,
            IconImge = menuItem.IconImge,
            Translate = menuItem.Translate,
            RouterLink = routerLink,
            SubId = menuItem.SubId,
            HashName = menuItem.HashName
        };
    }
}