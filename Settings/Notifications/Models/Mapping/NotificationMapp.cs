using AutoMapper;

namespace QuickFix.Settings.Notifications.Models.Mapping
{
    public class NotificationMapp : Profile
    {
        public NotificationMapp()
        {
            CreateMap<NotificationRequest, NotificationResponse>();
        }
    }
}
