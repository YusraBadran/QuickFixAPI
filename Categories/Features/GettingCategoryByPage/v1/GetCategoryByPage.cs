using AutoMapper;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models.DTOs;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Categories.Features.GettingCategoryByPage.v1;

public record GetCategoryByPage : ListQuery<GetCategoryByPageResponse>;
public class GetCategoryHandler : IQueryHandler<GetCategoryByPage, GetCategoryByPageResponse>
{
    private readonly ICategoryContext _category;
    private readonly IMapper _mapper;
    public GetCategoryHandler(ICategoryContext category, IMapper mapper)
    {
        _category = category;
        _mapper = mapper;
    }
    public async Task<GetCategoryByPageResponse> Handle(GetCategoryByPage request, CancellationToken cancellationToken)
    {
        var category = await _category.FindCategoryWithPageAsync<CategoryDTOs>(_mapper, request, cancellationToken);
        return new GetCategoryByPageResponse(category);
    }
}