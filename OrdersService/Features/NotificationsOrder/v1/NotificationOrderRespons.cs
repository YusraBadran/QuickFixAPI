using QuickFix.OrdersService.Models.DTOs;
using QuickFix.Settings.Notifications.Models;

namespace QuickFix.OrdersService.Features.NotificationsOrder.v1;

public record NotificationOrderRespons(List<NotificationRequest> notify)
{
}

