using AutoMapper;
using QuickFix.CategoriesItem.Features.CreateCategoriesItem.v1;
using QuickFix.CategoriesItem.Models.DTOs;

namespace QuickFix.CategoriesItem.Models.Mapping
{
    public class CategoryItemMapping : Profile
    {
        public CategoryItemMapping()
        {
            CreateMap<CategoryItems, CategoryItemDTO>()
            .ForMember(des => des.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<CategoryItems, CategoryItemByIdDTO>()
            .ForMember(des => des.Category, opt => opt.MapFrom(src => src.Category));
            //  .ForMember(des => des.image, opt => opt.MapFrom(src => ));
            // CreateMap<CategoryItems, CategoryItemDTO>();
        }
    }
}
