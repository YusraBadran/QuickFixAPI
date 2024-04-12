using AutoMapper;
using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Features.GettingCategoryNextBySubId.v1;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Models.DTOs;
using QuickFix.ServicesType.Features.GettingServicesTypeById.v1;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryPreviousBySubId.v1;

public record GetCategoryPreviousBySubId(Guid Id) : ITxCommand<GetCategoryPreviousBySubIdRespons>
{
}
public class Validator : AbstractValidator<GetCategoryPreviousBySubId>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class GetCategoryByIdHandler : ICommandHandler<GetCategoryPreviousBySubId, GetCategoryPreviousBySubIdRespons>
{
    private readonly ICategoryContext _context;
    private readonly IMapper _mapper;
    public GetCategoryByIdHandler(ICategoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryPreviousBySubIdRespons> Handle(GetCategoryPreviousBySubId request, CancellationToken cancellationToken)
    {
        var getSubId = await _context.FindCategoryById(request.Id);
        var category = await _context.FindAllCategoryBySubId((Guid)getSubId.SubCategoryId);
        var respons = _mapper.Map<IEnumerable<CategoryDTOs>>(category);
        return new GetCategoryPreviousBySubIdRespons(respons);
    }
}
