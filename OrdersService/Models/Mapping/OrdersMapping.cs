using AutoMapper;
using QuickFix.Settings.Notifications.Models;
using QuickFix.OrdersService.Models;
using QuickFix.OrdersService.Models.DTOs;

namespace QuickFix.OrdersService.Models.Mapping
{
    public class OrdersMapping : Profile
    {
        public OrdersMapping()
        {
            CreateMap<Orders, OrdersDto>()
                .ForMember(dest => dest.FullNameUser, opt => opt.MapFrom(src => src.User.FirstName + src.User.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Phone) ? src.User.PhoneNumber : src.Phone))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Orders, OrderDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Phone) ? src.User.PhoneNumber : src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
            CreateMap<OrderDetails, OrderDetailsDtos>()
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.CategoryItemId))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryItems.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.CategoryItems.Price))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryItems.Category.Name));
            CreateMap<Orders, NotificationsOrderDto>();
            CreateMap<Orders, NotificationRequest>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
               .ForMember(dest => dest.SubTitle, opt => opt.MapFrom(src => src.Phone))
               .ForMember(dest => dest.Time, opt => opt.MapFrom(src => $"{src.Date}"))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Note));

        }
    }
}
