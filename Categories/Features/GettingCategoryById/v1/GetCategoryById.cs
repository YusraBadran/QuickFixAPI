using AutoMapper;
using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Models.DTOs;
using QuickFix.ServicesType.Features.GettingServicesTypeById.v1;
using QuickFix.ServicesType.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategoryById.v1;

public record GetCategoryById(Guid Id) : ITxCommand<GetCategoryByIdRespons>
{
}
public class Validator : AbstractValidator<GetCategoryById>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
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
        if (respons.SubCategoryId != null)
        {
            var subcategory = await _context.FindCategoryById((Guid)respons.SubCategoryId);
            var subcategoryDto = _mapper.Map<LookUpCategoryRespons>(subcategory);
            respons.SubCategory = new LookUpCategoryRespons()
            {
                Id = subcategoryDto.Id,
                Name = subcategoryDto.Name,
            };
        }
        /*   respons.SubCategory.Id = subcategoryDto.Id;
           respons.SubCategory.Name = subcategoryDto.Name;*/

        return new GetCategoryByIdRespons(respons);
    }
}
