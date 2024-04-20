using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Exceptions;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Models;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Images.Data;
using QuickFix.Shared.Images.Extensions;
using QuickFix.Shared.Images.Models;
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
        RuleFor(C => C.Description).NotEmpty().NotNull().WithMessage("وصف الفائه مطلوب").MaximumLength(350).WithMessage("يجب ان لايتجاوز عن 350 حرف");
        RuleFor(C => C.Status).NotEmpty().NotNull().WithMessage("يجب تحديد الحالة");
        RuleFor(x => x.Price).NotEmpty().NotNull().WithMessage("السعر مطلوب").Must(x => x > 0).WithMessage("السعر يجب ان يكون اكبر من صفر");
        RuleFor(C => C.CategoryId).NotEmpty().NotNull().WithMessage(" يجب تحديد الفئة الرئيسية");
    }
}
public class UpdateCategoryItemHandler : ICommandHandler<UpdateCategoryItem, DataRespons>
{
    private readonly ICategoryItemContext _context;
    private readonly IImagContext _images;
    public UpdateCategoryItemHandler(ICategoryItemContext context, IImagContext image)
    {
        _context = context;
        _images = image;
    }
    public async Task<DataRespons> Handle(UpdateCategoryItem request, CancellationToken cancellationToken)
    {
               var categoryItem = await _context.FindCategoryItemById(request.Id);
        var nameEx = await _context.FindCategoryItemByName(request.Name);
        if (nameEx != null && nameEx.Id != categoryItem.Id)
        {
            throw new CategoryNameAlreadyExistException(request.Name);
        }

        categoryItem.Name = request.Name;

        categoryItem.Description = request.Description;

        categoryItem.Status = request.Status;
        categoryItem.Price = request.Price;
        categoryItem.CategoryId = request.CategoryId;
        var respons = await _context.UpdateAsync(categoryItem);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        var Images =await _images.FindAllCategoryItemImage(categoryItem.Id);
        if (Images != null)
        {
           var delet =await _images.DeleteImagAsync(Images);
            if (delet.StatusCode != 200)
            {
                throw new BadRequestException(delet.Message);
            }
          
        }
        var image = new List<Image>();
        foreach (var item in request.Image)
        {
            image.Add(new Image
            {
                Id = Guid.NewGuid(),
                HadImage = categoryItem.Id,
                Url = item
            });
        }
        var imageCreate = await _images.CreateImagAsync(image);
        if (imageCreate.StatusCode != 200)
        {
            throw new BadRequestException(imageCreate.Message);
        }
        throw new SuccessException(categoryItem.Id);
    }
}