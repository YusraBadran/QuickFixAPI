using System;
using AutoMapper;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Models.DTOs;

namespace QuickFix.Identity.Users.Models.GetUserByEmail.Maping
{
    public class UsersMapping:Profile
    {
        public UsersMapping ()
        {
            CreateMap<ApplicationUser,IdentityUserDto>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(des=> des.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(des=>des.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(des=>des.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(des=>des.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(des=>des.LastLoggedInAt, opt => opt.MapFrom(src => src.LastLoggedInAt))
            .ForMember(des=>des.RefreshTokens, opt => opt.MapFrom(src => src.RefreshTokens.Select(r=>r.Token)))
            .ForMember(des=>des.Roles, opt => opt.MapFrom(src => src.UserRoles.Where(ur=>ur.Role!=null).Select(uq=>uq.Role!.Name)))
            .ForMember(des=>des.UserState, opt => opt.MapFrom(src => src.UserState))
            .ForMember(des=>des.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
