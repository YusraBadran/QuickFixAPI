using AutoMapper;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Models.DTOs
{
    public record CategoryWithSubCatugoryDTOs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeStates State { get; set; }
        public string ServiceId { get; set; }
        public string? SubCategoryId { get; set; }
        public LookUpCategoryRespons? SubCategory { get; set; }
    }
}
