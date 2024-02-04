using QuickFix.Categories.Models;

namespace QuickFix.Categories.Features.CreateCategories.v1
{
    public record CreateCategoryRequest
    {
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public CategoryState State { get; set; }
        public Guid ServiceId { get; set; }
    }
}
