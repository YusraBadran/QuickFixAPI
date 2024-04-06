using AutoMapper;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Settings.Screens.Data;
using QuickFix.Settings.Screens.Extensions;

namespace QuickFix.Settings.Screens.Features.LookUpsScreen.v1;

public record LookUpScreen : ICommand<IEnumerable<LookUpScreenRespons>>
{
}
public class LookUpHandler : ICommandHandler<LookUpScreen, IEnumerable<LookUpScreenRespons>>
{
    private readonly IScreenContext _context;
    private readonly IMapper _mapper;
    public LookUpHandler(IScreenContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<LookUpScreenRespons>> Handle(LookUpScreen command, CancellationToken cancellationToken)
    {
        var data = await _context.FindAllScreen();
        var result = _mapper.Map<IEnumerable<LookUpScreenRespons>>(data);
        return result;
    }
}

