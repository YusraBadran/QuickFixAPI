using AutoMapper;
using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Models.DTOs;
using QuickFix.ServicesType.Features.GettingServicesTypeById.v1;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryBySubId.v1;

public record GetCategoryBySubId(Guid Id) : ITxCommand<GetCategoryBySubIdRespons>
{
}
public class Validator : AbstractValidator<GetCategoryBySubId>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class GetCategoryBySubIdHandler : ICommandHandler<GetCategoryBySubId, GetCategoryBySubIdRespons>
{
    private readonly ICategoryContext _context;
    private readonly IMapper _mapper;
    public GetCategoryBySubIdHandler(ICategoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryBySubIdRespons> Handle(GetCategoryBySubId request, CancellationToken cancellationToken)
    {
        var category = await _context.FindAllCategoryBySubId(request.Id);
        var respons = _mapper.Map<IEnumerable<CategoryDTOs>>(category);

        return new GetCategoryBySubIdRespons(respons);
    }
}
