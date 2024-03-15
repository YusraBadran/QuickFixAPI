using QuickFix.Categories.Models;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Features.UpdateCategoryItem.v1;

public record UpdateCategoryItemRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? NameEn { get; set; }
    public string? Description { get; set; }
    public string? DescriptionEn { get; set; }
    public TypeStates Status { get; set; }
    public double Price { get; set; }
    public Guid CategoryId { get; set; }
}
