using AutoMapper;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Exceptions;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.ServicesType.Features.GettingServicesType.v1;

public record GetServicesType : ITxCommand<GetServicesTypeRespons>;
public class GetServicesTypeHandler : ICommandHandler<GetServicesType, GetServicesTypeRespons>
{
    private readonly IServiceTypeContext _context;
    private readonly IMapper _mapper;
    public GetServicesTypeHandler(IServiceTypeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetServicesTypeRespons> Handle(GetServicesType request, CancellationToken cancellationToken)
    {
        var serviceType = await _context.FindAllServiceType();
        var respons = _mapper.Map<IEnumerable<ServicesTypeDTOs>>(serviceType);
        return new GetServicesTypeRespons(respons);
    }
}