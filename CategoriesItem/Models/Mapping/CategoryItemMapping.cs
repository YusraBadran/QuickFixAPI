using AutoMapper;
using QuickFix.CategoriesItem.Features.CreateCategoriesItem.v1;
using QuickFix.CategoriesItem.Models.DTOs;

namespace QuickFix.CategoriesItem.Models.Mapping
{
    public class CategoryItemMapping : Profile
    {
        public CategoryItemMapping()
        {
            CreateMap<CategoryItems, CategoryItemDTO>();
        }
    }
}
