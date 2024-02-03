using AutoMapper;
using QuickFix.Categories.Models.DTOs;

namespace QuickFix.Categories.Models.Mapping
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDTOs>();
        }
    }
}
