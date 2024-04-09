using QuickFix.Settings.Notifications.Models;

namespace QuickFix.Settings.Notifications.Interface;

public interface INotificationHub
{
    Task SendNotificationAsync(NotificationRequest notify);
    Task SendNewOrderNotificationAsync(NotificationRequest notify);
}