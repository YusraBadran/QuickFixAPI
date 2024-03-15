using AutoMapper;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.CategoriesItem.Features.GettingCategoriesItem.v1;

public record GetCategoryItem : ITxCommand<GetCategoryItemRespons>
{
}

public class GetCategoryItemHandler : ICommandHandler<GetCategoryItem, GetCategoryItemRespons>
{
    private readonly ICategoryItemContext _context;
    private readonly IMapper _mapper;
    public GetCategoryItemHandler(ICategoryItemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryItemRespons> Handle(GetCategoryItem request, CancellationToken cancellationToken)
    {
        var category = await _context.FindAllCategoryItem();
        var respons = _mapper.Map<IEnumerable<CategoryItemDTO>>(category);
        return new GetCategoryItemRespons(respons);
    }
}