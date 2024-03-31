using QuickFix.Categories.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Features.CreateCategories.v1
{
    public record CreateCategoryRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string? Logo { get; set; }
        public TypeStates State { get; set; } = TypeStates.unActive;
        public string? ServiceId { get; set; }
        public string? SubCategoryId { get; set; }
    }
}
