using System;
using AutoMapper;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Features.GettingUserById.v1;
using QuickFix.Identity.Users.Models.DTOs;
using QuickFix.Identity.Users.Models.DTOS.v1;

namespace QuickFix.Identity.Users.Models.GetUserByEmail.Maping
{
    public class UsersMapping : Profile
    {
        public UsersMapping()
        {
            CreateMap<ApplicationUser, IdentityUserDto>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                        .ForMember(des => des.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(des => des.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(des => des.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(des => des.UserState, opt => opt.MapFrom(src => src.UserState));
            CreateMap<ApplicationUser, GetUsersByIdResponse>()
  .ForMember(des => des.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(des => des.Roles, opt => opt.MapFrom(src => src.UserRoles.Where(m => m.Role != null).Select(q => q.Role!.Name)))
            .ForPath(des => des.Permissions, opt => opt.MapFrom(src => src.Role));
            CreateMap<ApplicationUser, OrderUserDto>()
         .ForMember(des => des.FullName, opt => opt.MapFrom(src => src.FirstName + src.LastName))
         .ForMember(des => des.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
