using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Exceptions;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Features.UpdateCategories.v1;

public record UpdateCategory : UpdateCategoryRequest, ITxCreateCommand<DataRespons>
{
    public UpdateCategory(UpdateCategoryRequest request) : base(request) { }
}
public class Validator : AbstractValidator<UpdateCategory>
{
    public Validator()
    {
        RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("اسم الفائه مطلوب");

        RuleFor(C => C.Description).NotEmpty().NotNull().WithMessage("وصف الفائه مطلوب").MaximumLength(350).WithMessage("يجب ان لايتجاوز عن 350 حرف");

        RuleFor(C => C.State).NotEmpty().NotNull().WithMessage("يجب تحديد الحالة");
        RuleFor(C => C.ServiceId).NotEmpty().NotNull().WithMessage("يجب تحديد الخدمة ");
        RuleFor(C => C.SubCategoryId).NotEmpty().NotNull().WithMessage("يجب تحديد الفئة الفرعية");
    }
}
public class CreateCategoryHandler : ICommandHandler<UpdateCategory, DataRespons>
{
    private readonly ICategoryContext _context;
    public CreateCategoryHandler(ICategoryContext context)
    {
        _context = context;
    }
    public async Task<DataRespons> Handle(UpdateCategory request, CancellationToken cancellationToken)
    {
        var category = await _context.FindCategoryById(request.Id);
        var nameEx = await _context.FindCategoryByName(category.Name);
        if (nameEx != null && nameEx.Id != category.Id)
        {
            throw new CategoryNameAlreadyExistException(request.Name);
        }
        category.Name = request.Name;
        category.Description = request.Description;
        category.Logo = request.Logo;
        category.State = request.State;
        category.ServiceId = request.ServiceId;
        category.SubCategoryId = request.SubCategoryId;
        var respons = await _context.UpdateAsync(category);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        throw new SuccessException(category.Id);
    }
}