using AutoMapper;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Images.Data;
using QuickFix.Shared.Images.Extensions;

namespace QuickFix.CategoriesItem.Features.GettingCategoriesItem.v1;

public record GetCategoryItem : ITxCommand<GetCategoryItemRespons>
{
}

public class GetCategoryItemHandler : ICommandHandler<GetCategoryItem, GetCategoryItemRespons>
{
    private readonly ICategoryItemContext _context;
    private readonly IImagContext _imageContext;
    private readonly IMapper _mapper;
    public GetCategoryItemHandler(ICategoryItemContext context, IMapper mapper, IImagContext imageContext)
    {
        _context = context;
        _mapper = mapper;
        _imageContext = imageContext;
    }
    public async Task<GetCategoryItemRespons> Handle(GetCategoryItem request, CancellationToken cancellationToken)
    {
        var categoryItem = await _context.FindAllCategoryItem();
        var images = await _imageContext.FindAllImage();
        var respons = _mapper.Map<IEnumerable<CategoryItemDTO>>(categoryItem);
        foreach (var item in respons)
        {
            item.Image = images.Where(x => x.HadImage == item.Id).ToList();
        }
        return new GetCategoryItemRespons(respons);
    }
}