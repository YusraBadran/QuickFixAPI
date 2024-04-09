using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Shared.Images.Models;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Models.DTOs
{
    public record CategoryItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string Description { get; set; }
        public TypeStates Status { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public LookUpCategoryRespons Category { get; set; }
        public IEnumerable<Image> Image { get; set; }
    }
}
