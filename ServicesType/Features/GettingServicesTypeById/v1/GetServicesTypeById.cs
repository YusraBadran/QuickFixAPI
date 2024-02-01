using AutoMapper;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Exceptions;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.ServicesType.Features.GettingServicesTypeById.v1;

public record GetServicesTypeById(Guid Id) : ITxCommand<GetServicesTypeByIdRespons>;
public class GetServicesTypeHandler : ICommandHandler<GetServicesTypeById, GetServicesTypeByIdRespons>
{
    private readonly IServiceTypeContext _context;
    private readonly IMapper _mapper;
    public GetServicesTypeHandler(IServiceTypeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetServicesTypeByIdRespons> Handle(GetServicesTypeById request, CancellationToken cancellationToken)
    {
        var serviceType = await _context.FindServiceTypeById(request.Id);
        var respons = _mapper.Map<ServicesTypeDTOs>(serviceType);
        return new GetServicesTypeByIdRespons(respons);
    }
}