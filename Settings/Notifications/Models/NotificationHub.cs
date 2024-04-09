using QuickFix.Identity.Shared.Models;
using QuickFix.Settings.Notifications.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace QuickFix.Settings.Notifications.Models;

public class NotificationHub : Hub<INotificationHub>
{
    public async Task SendNotificationToAllAsync(NotificationRequest notify)
    {
        await Clients.All.SendNotificationAsync(notify);
    }
    public async Task SendNewCompanyNotificationAsync(NotificationRequest notify)
    {
        string sendTo = "superNotify";


        await Clients.Group(sendTo).SendNotificationAsync(notify);
    }
    public async Task SendNotificationToUserAsync(Guid Id, NotificationRequest notify)
    {
        string userId = Id.ToString();
        await Clients.User(userId).SendNotificationAsync(notify);
    }
    public async Task SendNewOrderNotificationAsync(string branchId, NotificationRequest notify)
    {
        await Clients.Group(branchId).SendNotificationAsync(notify);
    }

    public async Task AddToGroupAsync(string groupName)
    {

        await this.Groups.AddToGroupAsync(
             this.Context.ConnectionId, groupName);
    }
}