using System;
using System.Net;
using FluentValidation;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Exceptions;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Features.CreateServicesType.v1;

public record CreateServiceType : CreateServiceTypeRequest, ITxCreateCommand<DataRespons>
{
    public CreateServiceType(CreateServiceTypeRequest request) : base(request)
    {

    }
}

public class Validator : AbstractValidator<CreateServiceType>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("الاسم مطلوب")
        .MaximumLength(60).WithMessage("الاسم لا يجب ان يتجاوز 60 حرف");
        RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("الوصف مطلوب")
        .MaximumLength(150).WithMessage("الوصف لا يجب ان يتجاوز 150 حرف");

    }
}
public class CreateServiceTypeHandler : ICommandHandler<CreateServiceType, DataRespons>
{
    private readonly IServiceTypeContext _context;
    public CreateServiceTypeHandler(IServiceTypeContext context)
    {
        _context = context;
    }
    public async Task<DataRespons> Handle(CreateServiceType request, CancellationToken cancellationToken)
    {
        var nameExist = await _context.FindServiceTypeByName(request.Name);

        if (nameExist != null)
        {
            throw new ServiceTypeNameAlreadyExist(nameExist.Name);
        }

        // var nameEnExist = await _context.FindServiceTypeByName(request.Name);

        // if (nameEnExist != null)
        // {
        //     throw new ServiceTypeNameAlreadyExist(nameEnExist.Name);
        // }

        var serviceType = new ServiceType
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Logo = request.Logo,
            Description = request.Description,

            Status = request.Status,
        };

        var result = await _context.CreateAsync(serviceType);

        if (result.StatusCode != 200)
        {
            throw new BadRequestException(result.Message);
        }


        throw new SuccessException((Guid)serviceType.Id);
    }
}
