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

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemById.v1;

public record GetCategoryItemById(Guid Id) : ITxCommand<GetCategoryItemByIdRespons>
{
}
public class Validator : AbstractValidator<GetCategoryItemById>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class GetCategoryByIdHandler : ICommandHandler<GetCategoryItemById, GetCategoryItemByIdRespons>
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
    public async Task<GetCategoryItemByIdRespons> Handle(GetCategoryItemById request, CancellationToken cancellationToken)
    {
        var category = await _context.FindCategoryItemById(request.Id);
        var respons = _mapper.Map<CategoryItemByIdDTO>(category);
        var images = await _imageContext.FindAllCategoryItemImage(request.Id);
        var imageDto = _mapper.Map<IEnumerable<ImageDtos>>(images);
        respons.Image = imageDto.Select(x => x.Url).ToList();

        return new GetCategoryItemByIdRespons(respons);
    }
}
