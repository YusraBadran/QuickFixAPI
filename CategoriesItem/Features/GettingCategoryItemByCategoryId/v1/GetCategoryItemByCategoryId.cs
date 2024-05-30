using AutoMapper;
using FluentValidation;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Images.Data;
using QuickFix.Shared.Images.Extensions;
using QuickFix.Shared.Images.Models.DTOs;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemByCategoryId.v1;

public record GetCategoryItemByCategoryId(Guid Id) : ITxCommand<GetCategoryItemByCategoryIdRespons>
{
}
public class Validator : AbstractValidator<GetCategoryItemByCategoryId>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class GetCategoryByIdHandler : ICommandHandler<GetCategoryItemByCategoryId, GetCategoryItemByCategoryIdRespons>
{
    private readonly ICategoryItemContext _context;
    private readonly IMapper _mapper;
    private readonly IImagContext _imageContext;
    public GetCategoryByIdHandler(ICategoryItemContext context, IMapper mapper, IImagContext imageContext)
    {
        _context = context;
        _mapper = mapper;
        _imageContext = imageContext;
    }
    public async Task<GetCategoryItemByCategoryIdRespons> Handle(GetCategoryItemByCategoryId request, CancellationToken cancellationToken)
    {
        var category = await _context.FindCategoryItemByCategoryId(request.Id);
        var respons = _mapper.Map<List<CategoryItemDTOs>>(category);
        foreach (var item in respons)
        {
            var images = await _imageContext.FindAllCategoryItemImage(item.Id);
            var imageDto = _mapper.Map<IEnumerable<ImageDtos>>(images);
            item.Image = imageDto.Select(x => x.Url).ToList();
            item.Logo = imageDto.FirstOrDefault(x => x.Url != null).Url;
        }

        return new GetCategoryItemByCategoryIdRespons(respons);
    }
}
