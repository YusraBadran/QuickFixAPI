using AutoMapper;
using FluentValidation;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.CategoriesItem.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.CategoriesItem.Features.GettingCategoryItemById.v1;

public record GetCategoryItemById(Guid Id) : ITxCommand<GetCategoryItemByIdRespons>
{
}
public class Validator : AbstractValidator<GetCategoryItemById>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class GetCategoryByIdHandler : ICommandHandler<GetCategoryItemById, GetCategoryItemByIdRespons>
{
    private readonly ICategoryItemContext _context;
    private readonly IMapper _mapper;
    public GetCategoryByIdHandler(ICategoryItemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryItemByIdRespons> Handle(GetCategoryItemById request, CancellationToken cancellationToken)
    {
        var category = await _context.FindCategoryItemById(request.Id);
        var respons = _mapper.Map<CategoryItemDTO>(category);
        return new GetCategoryItemByIdRespons(respons);
    }
}
