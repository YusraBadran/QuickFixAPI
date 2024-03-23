using FluentValidation;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Exceptions;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Models;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Features.CreateCategoriesItem.v1;

public record CreateCategoryItem : CreateCategoryItemRequest, ITxCreateCommand<DataRespons>
{
    public CreateCategoryItem(CreateCategoryItemRequest request) : base(request)
    {
    }
}
public class Validator : AbstractValidator<CreateCategoryItem>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("الاسم مطلوب")
        .MaximumLength(50).WithMessage("الاسم لا يجب ان يتجاوز 60 حرف");

        RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("الوصف مطلوب")
        .MaximumLength(350).WithMessage("الوصف لا يجب ان يتجاوز 350 حرف");

        RuleFor(x => x.Price).NotEmpty().NotNull().WithMessage("السعر مطلوب").Must(x => x > 0).WithMessage("السعر يجب ان يكون اكبر من صفر");
    }
}
public class CreateCategoryItemHandler : ICommandHandler<CreateCategoryItem, DataRespons>
{
    private readonly ICategoryItemContext _context;
    public CreateCategoryItemHandler(ICategoryItemContext context)
    {
        _context = context;
    }
    public async Task<DataRespons> Handle(CreateCategoryItem request, CancellationToken cancellationToken)
    {
        /// Check if the name already exist
        var nameExist = await _context.FindCategoryItemByName(request.Name);
        if (nameExist != null)
        {
            throw new CategoryItemNameAlreadyExist(nameExist.Name);
        }
        /// Check if the nameEn already exist
        var nameEnExist = await _context.FindCategoryItemByName(request.NameEn);
        if (nameEnExist != null)
        {
            throw new CategoryItemNameAlreadyExist(nameEnExist.Name);
        }
        var categoryItem = new CategoryItems
        {
            Id = Guid.NewGuid(),
            Name = request.Name,

            Description = request.Description,
            Status = request.Status,
            Price = request.Price,
            CategoryId = string.IsNullOrEmpty(request.CategoryId) ? null : Guid.Parse(request.CategoryId)

        };
        var create = await _context.CreateAsync(categoryItem);
        if (create.StatusCode != 200)
        {
            throw new BadRequestException(create.Message);
        }

        throw new SuccessException(categoryItem.Id);
    }
}

