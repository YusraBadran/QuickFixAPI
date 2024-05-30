using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Models.DTOs
{
    public class CategoryItemDTOs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public TypeStates Status { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<string> Image { get; set; }
    }
}
