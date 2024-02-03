using AutoMapper;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models.DTOs;
using QuickFix.ServicesType.Features.GettingServicesTypeById.v1;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryById.v1;

public record GetCategoryById(Guid Id) : ITxCommand<GetCategoryByIdRespons>
{
}

public class GetCategoryByIdHandler : ICommandHandler<GetCategoryById, GetCategoryByIdRespons>
{
    private readonly ICategoryContext _context;
    private readonly IMapper _mapper;
    public GetCategoryByIdHandler(ICategoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryByIdRespons> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        var category = await _context.FindCategoryById(request.Id);
        var respons = _mapper.Map<CategoryDTOs>(category);
        return new GetCategoryByIdRespons(respons);
    }
}
