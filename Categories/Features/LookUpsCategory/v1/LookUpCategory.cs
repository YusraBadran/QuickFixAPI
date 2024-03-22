using AutoMapper;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace QuickFix.Categories.Features.LookUpsCategory.v1;

public record LookUpCategory : ICommand<IEnumerable<LookUpCategoryRespons>>
{
}
public class LookUpHandler : ICommandHandler<LookUpCategory, IEnumerable<LookUpCategoryRespons>>
{
    private readonly ICategoryContext _context;
    private readonly IMapper _mapper;
    public LookUpHandler(ICategoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<IEnumerable<LookUpCategoryRespons>> Handle(LookUpCategory command, CancellationToken cancellationToken)
    {
        var data = await _context.FindAllCategory();
        var result = _mapper.Map<IEnumerable<LookUpCategoryRespons>>(data);
        return result;
    }
}

