using AutoMapper;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Features.GettingCategoryItemByPage.v1;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemByPage.v1;

public record GetCategoryItemByPage : ListQuery<GetCategoryItemByPageResponse>;
public class GetCategoryHandler : IQueryHandler<GetCategoryItemByPage, GetCategoryItemByPageResponse>
{
    private readonly ICategoryItemContext _category;
    private readonly IMapper _mapper;
    public GetCategoryHandler(ICategoryItemContext category, IMapper mapper)
    {
        _category = category;
        _mapper = mapper;
    }
    public async Task<GetCategoryItemByPageResponse> Handle(GetCategoryItemByPage request, CancellationToken cancellationToken)
    {
        var category = await _category.FindCategoryItemWithPageAsync<CategoryItemDTO>(_mapper, request, cancellationToken);
        return new GetCategoryItemByPageResponse(category);
    }
}