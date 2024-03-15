using AutoMapper;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Models.DTOs
{
    public record CategoryDTOs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public TypeStates State { get; set; }
        public string ServiceId { get; set; }
        public string SubCategoryId { get; set; }
    }
}
