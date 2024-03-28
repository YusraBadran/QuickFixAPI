using QuickFix.CategoriesItem.Models;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Models
{
    public record Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
public string? Logo { get; set; }
        public TypeStates State { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? SubCategoryId { get; set; } = null;
        public ServiceType ServiceType { get; set; }
        public ICollection<CategoryItems> CategoryItems { get; set; }
    }
}
