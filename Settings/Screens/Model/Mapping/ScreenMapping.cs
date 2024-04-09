using AutoMapper;
using QuickFix.Settings.Screens.Features.LookUpsScreen.v1;
using QuickFix.Settings.Screens.Model;
using QuickFix.Settings.Screens.Model.DTOs;

namespace QuickFix.Settings.Screens.Model.Mapping
{
    public class ScreenMapping : Profile
    {
        public ScreenMapping()
        {
            CreateMap<ScreenModel, ScreenDTO>();
            CreateMap<ScreenModel, LookUpScreenRespons>();
            /*      CreateMap<ScreenModel, UserScreenDTO>()
                      .ForMember(des => des.translate, opt => opt.MapFrom(src => src.translate))
                      .ForMember(des => des.hashName, opt => opt.MapFrom(src => src.hashName));*/
            CreateMap<UserScreen, UserScreenDTO>()
                   .ForMember(des => des.Translate, opt => opt.MapFrom(src => src.Screen.Translate))
                .ForMember(des => des.HashName, opt => opt.MapFrom(src => src.Screen.HashName));
        }
    }
}
