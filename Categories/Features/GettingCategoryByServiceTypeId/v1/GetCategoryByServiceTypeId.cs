using AutoMapper;
using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models.DTOs;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryByServiceTypeId.v1;

public record GetCategoryByServiceTypeId(Guid Id) : ITxCommand<GetCategoryByServiceTypeIdRespons>
{
}
public class Validator : AbstractValidator<GetCategoryByServiceTypeId>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الخدمة");
    }
}
public class GetCategoryByServiceTypeIdHandler : ICommandHandler<GetCategoryByServiceTypeId, GetCategoryByServiceTypeIdRespons>
{
    private readonly ICategoryContext _context;
    private readonly IMapper _mapper;
    public GetCategoryByServiceTypeIdHandler(ICategoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryByServiceTypeIdRespons> Handle(GetCategoryByServiceTypeId request, CancellationToken cancellationToken)
    {
        var category = await _context.FindAllCategoryByServiceTypeId(request.Id);
        var respons = _mapper.Map<IEnumerable<CategoryDtos>>(category);
        return new GetCategoryByServiceTypeIdRespons(respons);
    }
}
