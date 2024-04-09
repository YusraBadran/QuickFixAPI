using QuickFix.Settings.Notifications.Models;

namespace QuickFix.OrdersService.Models.DTOs
{
    public record NotificationsOrderDto(List<NotificationRequest> notify);
}
