using AutoMapper;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Exceptions;

namespace QuickFix.Categories.Features.LookUpsServiceType.v1;

public record LookUpServiceType : ICommand<IEnumerable<LookUpServiceTypeResponse>>
{
}
public class LookUpHandler : ICommandHandler<LookUpServiceType, IEnumerable<LookUpServiceTypeResponse>>
{
    private readonly IServiceTypeContext _context;
    private readonly IMapper _mapper;
    public LookUpHandler(IServiceTypeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<LookUpServiceTypeResponse>> Handle(LookUpServiceType command, CancellationToken cancellationToken)
    {
        var data = await _context.FindAllServiceType();
        var result = _mapper.Map<IEnumerable<LookUpServiceTypeResponse>>(data);
        return result;
    }
}

