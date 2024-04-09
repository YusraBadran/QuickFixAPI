using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuickFix.Settings.Notifications.Models
{
    public class NotificationRequest
    {

        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Time { get; set; }
    }
}
