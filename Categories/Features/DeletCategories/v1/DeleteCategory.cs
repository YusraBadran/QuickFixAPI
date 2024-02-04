using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Features.DeletCategories.v1;

public record DeleteCategory(Guid Id) : ITxCommand<DataRespons>;
public class DeleteCategoryHandler : ICommandHandler<DeleteCategory, DataRespons>
{
    private readonly ICategoryContext _category;
    public DeleteCategoryHandler(ICategoryContext category)
    {
        _category = category;
    }
    public async Task<DataRespons> Handle(DeleteCategory request, CancellationToken cancellationToken)
    {
        var category = await _category.FindCategoryById(request.Id);
        if (category == null)
        {
            //return new 
        }
        var respons = await _category.DeleteAsync(category);
        throw new SuccessException(category.Id);
    }
}