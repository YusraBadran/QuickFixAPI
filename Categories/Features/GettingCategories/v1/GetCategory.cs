using AutoMapper;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategories.v1;

public record GetCategory : ITxCommand<GetCategoryRespons>
{
}
public class GetCategoryHandler : ICommandHandler<GetCategory, GetCategoryRespons>
{
    private readonly ICategoryContext _context;
    private readonly IMapper _mapper;
    public GetCategoryHandler(ICategoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryRespons> Handle(GetCategory request, CancellationToken cancellationToken)
    {
        var category = await _context.FindAllCategory();
        var respons = _mapper.Map<IEnumerable<CategoryDTOs>>(category);
        return new GetCategoryRespons(respons);
    }
}