using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Exceptions;
using QuickFix.Categories.Extensions;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Features.DeletCategories.v1;

public record DeleteCategoryItem(Guid Id) : ITxCommand<DataRespons>;
public class Validator : AbstractValidator<DeleteCategoryItem>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class DeleteCategoryItemHandler : ICommandHandler<DeleteCategoryItem, DataRespons>
{
    private readonly ICategoryItemContext _category;
    public DeleteCategoryItemHandler(ICategoryItemContext category)
    {
        _category = category;
    }
    public async Task<DataRespons> Handle(DeleteCategoryItem request, CancellationToken cancellationToken)
    {
        var category = await _category.FindCategoryItemById(request.Id);
        if (category == null)
        {
            throw new CategoryNotFoundException();
        }
        var respons = await _category.DeleteAsync(category);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        throw new SuccessException(category.Id);
    }
}