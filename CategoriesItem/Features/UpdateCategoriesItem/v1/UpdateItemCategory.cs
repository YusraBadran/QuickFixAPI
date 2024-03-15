using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Exceptions;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Features.UpdateCategoryItem.v1;

public record UpdateCategoryItem : UpdateCategoryItemRequest, ITxCreateCommand<DataRespons>
{
    public UpdateCategoryItem(UpdateCategoryItemRequest request) : base(request) { }
}
public class Validator : AbstractValidator<UpdateCategoryItem>
{
    public Validator()
    {
        RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("اسم الفائه مطلوب");
        RuleFor(c => c.NameEn).NotEmpty().NotNull().WithMessage(" اسم الفائه بالانجليزي مطلوب");
        RuleFor(C => C.Description).NotEmpty().NotNull().WithMessage("وصف الفائه مطلوب").MaximumLength(350).WithMessage("يجب ان لايتجاوز عن 350 حرف");
        RuleFor(C => C.DescriptionEn).NotEmpty().NotNull().WithMessage(" وصف الفائه بالانجليزي مطلوب").MaximumLength(350).WithMessage("يجب ان لايتجاوز عن 350 حرف");
        RuleFor(C => C.Status).NotEmpty().NotNull().WithMessage("يجب تحديد الحالة");
        RuleFor(x => x.Price).NotEmpty().NotNull().WithMessage("السعر مطلوب").Must(x => x > 0).WithMessage("السعر يجب ان يكون اكبر من صفر");
        RuleFor(C => C.CategoryId).NotEmpty().NotNull().WithMessage(" يجب تحديد الفئة الرئيسية");
    }
}
public class CreateCategoryHandler : ICommandHandler<UpdateCategoryItem, DataRespons>
{
    private readonly ICategoryItemContext _context;
    public CreateCategoryHandler(ICategoryItemContext context)
    {
        _context = context;
    }
    public async Task<DataRespons> Handle(UpdateCategoryItem request, CancellationToken cancellationToken)
    {
        var category = await _context.FindCategoryItemById(request.Id);
        var nameEx = await _context.FindCategoryItemByName(request.Name);
        if (nameEx != null && nameEx.Id != category.Id)
        {
            throw new CategoryNameAlreadyExistException(request.Name);
        }
        var nameEnEx = await _context.FindCategoryItemByName(request.NameEn);
        if (nameEnEx != null && nameEnEx.Id != category.Id)
        {
            throw new CategoryNameAlreadyExistException(request.NameEn);
        }
        category.Name = request.Name;
        category.NameEn = request.NameEn;
        category.Description = request.Description;
        category.DescriptionEn = request.DescriptionEn;
        category.Status = request.Status;
        category.Price = request.Price;
        category.CategoryId = request.CategoryId;
        var respons = await _context.UpdateAsync(category);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        throw new SuccessException(category.Id);
    }
}