using QuickFix.Categories.Models;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Models
{
    public record CategoryItems
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public TypeStates Status { get; set; }
        public double Price { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
