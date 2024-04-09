using AutoMapper;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Features.GettingCategoryItemByPage.v1;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.MaintenanceCenters.Data;
using QuickFix.MaintenanceCenters.Extensions;
using QuickFix.MaintenanceCenters.Module;
using QuickFix.MaintenanceCenters.Module.DTOs;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.MaintenanceCenters.Features.GettingCentersByPage.v1;

public record GetCenterByPage : ListQuery<GetCenterByPageResponse>;
public class GetCategoryHandler : IQueryHandler<GetCenterByPage, GetCenterByPageResponse>
{
    private readonly ICentersDbContext _context;
    private readonly IMapper _mapper;
    public GetCategoryHandler(ICentersDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCenterByPageResponse> Handle(GetCenterByPage request, CancellationToken cancellationToken)
    {
        var center = await _context.FindCentersWithPageAsync<CentersDTOs>(_mapper, request, cancellationToken);
        return new GetCenterByPageResponse(center);
    }
}