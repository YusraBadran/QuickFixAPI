using AutoMapper;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.MaintenanceCenters.Data;
using QuickFix.MaintenanceCenters.Extensions;
using QuickFix.MaintenanceCenters.Module.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.MaintenanceCenters.Features.GettingCenters.v1;

public record GetCenter : ITxCommand<GetCenterRespons>
{
}

public class GetCenterHandler : ICommandHandler<GetCenter, GetCenterRespons>
{
    private readonly ICentersDbContext _context;
    private readonly IMapper _mapper;
    public GetCenterHandler(ICentersDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCenterRespons> Handle(GetCenter request, CancellationToken cancellationToken)
    {
        var center = await _context.FindAllCentersAsync();
        var respons = _mapper.Map<IEnumerable<CentersDTOs>>(center);
        return new GetCenterRespons(respons);
    }
}