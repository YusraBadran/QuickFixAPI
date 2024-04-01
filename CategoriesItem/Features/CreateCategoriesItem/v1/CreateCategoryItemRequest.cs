using QuickFix.Shared.Images.Models.DTOs;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Features.CreateCategoriesItem.v1
{
    public record CreateCategoryItemRequest
    {
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public TypeStates Status { get; set; } = TypeStates.unActive;
        public double Price { get; set; }
        public string? CategoryId { get; set; } = string.Empty;
        public List<string> Image { get; set; }
    }
}
