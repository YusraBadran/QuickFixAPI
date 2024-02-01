using AutoMapper;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Exceptions;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.ServicesType.Features.GettingServicesTypeByPage.v1;

public record GetServicesTypeByPage : ListQuery<GetServicesTypeByPageRespons>;
public class GetServicesTypeHandler : IQueryHandler<GetServicesTypeByPage, GetServicesTypeByPageRespons>
{
    private readonly IServiceTypeContext _context;
    private readonly IMapper _mapper;
    public GetServicesTypeHandler(IServiceTypeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetServicesTypeByPageRespons> Handle(GetServicesTypeByPage request, CancellationToken cancellationToken)
    {
        var serviceType = await _context.FindServiceWithPageAsync<ServicesTypeDTOs>(_mapper, request, cancellationToken);

        return new GetServicesTypeByPageRespons(serviceType);
    }
}