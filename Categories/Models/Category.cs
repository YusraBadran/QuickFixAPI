using QuickFix.ServicesType.Models;

namespace QuickFix.Categories.Models
{
    public record Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryState State { get; set; }
        public Guid ServiceId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}
