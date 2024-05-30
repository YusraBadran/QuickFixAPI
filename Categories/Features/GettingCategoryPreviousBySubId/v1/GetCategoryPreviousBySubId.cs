using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Models.DTOs;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Exceptions;
using QuickFix.ServicesType.Features.GettingServicesType.v1;
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
    private readonly IServiceTypeContext _service;
    private readonly ICommandProcessor _sender;
    public GetCategoryByIdHandler(ICategoryContext context, IMapper mapper, ICommandProcessor sender, IServiceTypeContext service)
    {
        _context = context;
        _mapper = mapper;
        _sender = sender;
        _service = service;
    }
    public async Task<GetCategoryPreviousBySubIdRespons> Handle(GetCategoryPreviousBySubId request, CancellationToken cancellationToken)
    {
        var getSubId = await _context.FindCategoryById(request.Id);
        if (getSubId.SubCategoryId == null)
        {
            var categoreis = await _context.FindAllCategoryByServiceTypeId((Guid)getSubId.ServiceId);
            var response = _mapper.Map<IEnumerable<CategoryDtos>>(categoreis);
            /* var serviceType = await _service.FindAllServiceType();
             var response = _mapper.Map<IEnumerable<ServicesTypeDTOs>>(serviceType);*/
            return new GetCategoryPreviousBySubIdRespons(response);
        }
        var category = await _context.FindAllCategoryBySubId((Guid)getSubId.SubCategoryId);
        var respons = _mapper.Map<IEnumerable<CategoryDtos>>(category);
        return new GetCategoryPreviousBySubIdRespons(respons);
    }
}