using AutoMapper;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Models.DTOs;

namespace QuickFix.Categories.Models.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDTOs>()
               .ForMember(des => des.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
               .ForMember(des => des.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
               .ForMember(des => des.ServiceType, opt => opt.MapFrom(src => src.ServiceType));
            CreateMap<Category, CategoryDtos>()
               .ForMember(des => des.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
               .ForMember(des => des.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
               .ForMember(des => des.HasCategoryItem, opt => opt.MapFrom(src => src.CategoryItems.Count() > 0 ? true : false));
            CreateMap<Category, LookUpCategoryRespons>();

        }
    }
}
