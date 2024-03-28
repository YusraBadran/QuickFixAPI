using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Exceptions;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Features.CreateCategories.v1;

public record CreateCategory : CreateCategoryRequest, ITxCreateCommand<DataRespons>
{
    public CreateCategory(CreateCategoryRequest request) : base(request) { }
}
public class Validator : AbstractValidator<CreateCategory>
{
    public Validator()
    {
        RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("اسم الفائه مطلوب");
        RuleFor(C => C.Description).NotEmpty().NotNull().WithMessage("وصف الفائه مطلوب").MaximumLength(350).WithMessage("يجب ان لايتجاوز عن 350 حرف");

        //RuleFor(C => C.SubCategoryId).Equal("string").WithMessage("يجب تحديد الفئة الرئيسية");
        //RuleFor(C => C.ServiceId).Equal("string").WithMessage("يجب تحديد الخدمة");
    }
}
public class CreateCategoryHandler : ICommandHandler<CreateCategory, DataRespons>
{
    private readonly ICategoryContext _context;
    public CreateCategoryHandler(ICategoryContext context)
    {
        _context = context;
    }
    public async Task<DataRespons> Handle(CreateCategory request, CancellationToken cancellationToken)
    {
        var nameEx = await _context.FindCategoryByName(request.Name);
        if (nameEx != null)
        {
            throw new CategoryNameAlreadyExistException(request.Name);
        }

        var category = new Category()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Logo=request.Logo,
            State = request.State,
            ServiceId = string.IsNullOrEmpty(request.ServiceId) ? null : Guid.Parse(request.ServiceId),
            SubCategoryId = string.IsNullOrEmpty(request.SubCategoryId) ? null : Guid.Parse(request.SubCategoryId)
        };
        var respons = await _context.CreateAsync(category);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        throw new SuccessException(category.Id);
    }
}