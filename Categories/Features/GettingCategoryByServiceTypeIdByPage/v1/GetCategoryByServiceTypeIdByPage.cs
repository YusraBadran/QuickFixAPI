using AutoMapper;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models.DTOs;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Categories.Features.GettingCategoryByServiceTypeIdByPage.v1;

public record GetCategoryByServiceTypeIdByPage(Guid Id) : ListQuery<GetCategoryByServiceTypeIdByPageResponse>;
public class GetCategoryHandler : IQueryHandler<GetCategoryByServiceTypeIdByPage, GetCategoryByServiceTypeIdByPageResponse>
{
    private readonly ICategoryContext _category;
    private readonly IMapper _mapper;
    public GetCategoryHandler(ICategoryContext category, IMapper mapper)
    {
        _category = category;
        _mapper = mapper;
    }
    public async Task<GetCategoryByServiceTypeIdByPageResponse> Handle(GetCategoryByServiceTypeIdByPage request, CancellationToken cancellationToken)
    {
        var category = await _category.FindCategoryByServicTypeIdWithPageAsync<CategoryDTOs>(_mapper, request, request.Id, cancellationToken);
        return new GetCategoryByServiceTypeIdByPageResponse(category);
    }
}