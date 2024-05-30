using AutoMapper;
using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Models.DTOs;
using QuickFix.ServicesType.Features.GettingServicesTypeById.v1;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryNextBySubId.v1;

public record GetCategoryNextBySubId(Guid Id) : ITxCommand<GetCategoryNextBySubIdRespons>
{
}
public class Validator : AbstractValidator<GetCategoryNextBySubId>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class GetCategoryByIdHandler : ICommandHandler<GetCategoryNextBySubId, GetCategoryNextBySubIdRespons>
{
    private readonly ICategoryContext _context;
    private readonly IMapper _mapper;
    public GetCategoryByIdHandler(ICategoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryNextBySubIdRespons> Handle(GetCategoryNextBySubId request, CancellationToken cancellationToken)
    {
        var category = await _context.FindAllCategoryBySubId(request.Id);
        var respons = _mapper.Map<IEnumerable<CategoryDtos>>(category);
        return new GetCategoryNextBySubIdRespons(respons);
    }
}
