using AutoMapper;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Features.GettingCategoryItemByPage.v1;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;
using QuickFix.Shared.Images.Data;
using QuickFix.Shared.Images.Extensions;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemByPage.v1;

public record GetCategoryItemByPage : ListQuery<GetCategoryItemByPageResponse>;
public class GetCategoryHandler : IQueryHandler<GetCategoryItemByPage, GetCategoryItemByPageResponse>
{
    private readonly ICategoryItemContext _context;
    private readonly IMapper _mapper;
    private readonly IImagContext _imageContext;
    public GetCategoryHandler(ICategoryItemContext context, IMapper mapper, IImagContext imageContext)
    {
        _context = context;
        _mapper = mapper;
        _imageContext = imageContext;
    }
    public async Task<GetCategoryItemByPageResponse> Handle(GetCategoryItemByPage request, CancellationToken cancellationToken)
    {
        var categoryItem = await _context.FindCategoryItemWithPageAsync<CategoryItemDTO>(_mapper, request, cancellationToken);
        var images = await _imageContext.FindAllImage();
        foreach (var item in categoryItem.Data)
        {
            item.Image = images.Where(x => x.HadImage == item.Id).ToList();
        }
        return new GetCategoryItemByPageResponse(categoryItem);
    }
}