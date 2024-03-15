using QuickFix.Categories.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Features.UpdateCategories.v1
{
    public record UpdateCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public TypeStates State { get; set; } = TypeStates.unActive;
        public Guid ServiceId { get; set; }
        public Guid? SubCategoryId { get; set; }
    }
}
