using FluentValidation;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Exceptions;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Features.UpdateServicesType.v1;

public record UpdateServiceType : UpdateServiceTypeRequest, ITxUpdateCommand<DataRespons>
{
    public UpdateServiceType(UpdateServiceTypeRequest request) : base(request)
    {
    }
}
public class Validator : AbstractValidator<UpdateServiceType>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("الاسم مطلوب")
        .MaximumLength(60).WithMessage("الاسم لا يجب ان يتجاوز 60 حرف");
        RuleFor(x => x.NameEn).NotEmpty().NotNull().WithMessage("الاسم بالانجليزي مطلوب")
        .MaximumLength(60).WithMessage("الاسم بالانجليزي لا يجب ان يتجاوز 60 حرف");
        RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("الوصف مطلوب")
        .MaximumLength(150).WithMessage("الوصف لا يجب ان يتجاوز 150 حرف");
        RuleFor(x => x.DescriptionEn).NotEmpty().NotNull().WithMessage("الوصف بالانجليزي مطلوب")
        .MaximumLength(150).WithMessage("الوصف بالانجليزي لا يجب ان يتجاوز 150 حرف");
    }
}
public class UpdateServiceTypeHandler : ICommandHandler<UpdateServiceType, DataRespons>
{
    private readonly IServiceTypeContext _context;
    public UpdateServiceTypeHandler(IServiceTypeContext context)
    {
        _context = context;
    }
    public async Task<DataRespons> Handle(UpdateServiceType request, CancellationToken cancellationToken)
    {
        var serviceType = await _context.FindServiceTypeById(request.Id);
        var nameExist = await _context.FindServiceTypeByName(request.Name);

        if (nameExist != null && nameExist.Id != serviceType.Id)
        {
            throw new ServiceTypeNameAlreadyExist(nameExist.Name);
        }

        // var nameEnExist = await _context.FindServiceTypeByName(request.NameEn);

        // if (nameEnExist != null && serviceType.Id == request.Id)
        // {
        //     throw new ServiceTypeNameAlreadyExist(nameEnExist.Name);
        // }

        serviceType.Name = request.Name;

        serviceType.Description = request.Description;

        serviceType.Status = request.Status;
        await _context.UpdateAsync(serviceType);

        throw new SuccessException((Guid)serviceType.Id);
    }
}
