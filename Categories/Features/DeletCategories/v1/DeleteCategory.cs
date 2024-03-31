using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Exceptions;
using QuickFix.Categories.Extensions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Features.DeletCategories.v1;

public record DeleteCategory(Guid Id) : ITxCommand<DataRespons>;
public class Validator : AbstractValidator<DeleteCategory>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
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
            throw new CategoryNotFoundException();
        }
        var hasChild = await _category.FindAllCategoryItemBySubId(category.Id);
        if (hasChild != null)
        {
            throw new BadRequestException("لايمكن حذف الفئة لانها تحتوي على فئات فرعية");
        }
        var respons = await _category.DeleteAsync(category);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        throw new SuccessException(category.Id);
    }
}