using AutoMapper;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Features.LookUpsServiceType.v1;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Models.DTOs
{
    public record CategoryDtos
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Logo { get; set; }
        public TypeStates State { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public bool HasCategoryItem { get; set; }
    }
}
