using AutoMapper;
using FluentValidation;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.MaintenanceCenters.Data;
using QuickFix.MaintenanceCenters.Extensions;
using QuickFix.MaintenanceCenters.Module.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.MaintenanceCenters.Features.GettingCentersById.v1;

public record GetCenterById(Guid Id) : ITxCommand<GetCenterByIdRespons>
{
}
public class Validator : AbstractValidator<GetCenterById>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class GetCategoryByIdHandler : ICommandHandler<GetCenterById, GetCenterByIdRespons>
{
    private readonly ICentersDbContext _context;
    private readonly IMapper _mapper;
    public GetCategoryByIdHandler(ICentersDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCenterByIdRespons> Handle(GetCenterById request, CancellationToken cancellationToken)
    {
        var center = await _context.FindCentersById(request.Id);
        var respons = _mapper.Map<CentersDTOs>(center);
        return new GetCenterByIdRespons(respons);
    }
}
