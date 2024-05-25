using QuickFix.Settings.Notifications.Models;

namespace QuickFix.Settings.Notifications.Interface;

public interface INotificationHub
{
    Task SendNotificationAsync(string notify);
    Task SendNewOrderNotificationAsync(NotificationRequest notify);
}