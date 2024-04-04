using QuickFix.Addresses.Models;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Module;
using FluentValidation;
using QuickFix.Addresses.Data;
using QuickFix.Addresses.Extensions;
using System.Net;
using QuickFix.MaintenanceCenters.Module;
using QuickFix.Addresses.Features.CreateAddresses.v1;
using QuickFix.Addresses.Models.DTOs;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.MaintenanceCenters.Data;
using QuickFix.MaintenanceCenters.Extensions;

namespace QuickFix.Addresses.Features.CreateCenters.v1;

public record CreateCenter : CreateCentersRequest, ITxCreateCommand<DataRespons>
{
    public CreateCenter(CreateCentersRequest request) : base(request) { }

}
public class Validate : AbstractValidator<CreateCenter>
{
    public Validate()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage(" الاسم مطلوب ");
        RuleFor(v => v.Description).NotEmpty().WithMessage(" الوصف مطلوب ");
        RuleFor(v => v.Address.Longitude).NotEmpty().WithMessage(" خط الطول مطلوب ");
        RuleFor(v => v.Address.Latitude).NotEmpty().WithMessage(" خط العرض مطلوب ");
    }
}
public class CreateAddressHandler : ICommandHandler<CreateCenter, DataRespons>
{
    private readonly ICentersDbContext _centersContext;
    private readonly IAddressDbContext _addressContext;
    private readonly ICommandProcessor _sender;
    public CreateAddressHandler(ICentersDbContext centersContext, ICommandProcessor sender, IAddressDbContext addressContext)
    {
        _centersContext = centersContext;
        _sender = sender;
        _addressContext = addressContext;
    }

    public async Task<DataRespons> Handle(CreateCenter request, CancellationToken cancellationToken)
    {
        var address = new CreateAddresRequest()
        {
            Location = request.Address.Location,
            Longitude = request.Address.Longitude,
            Latitude = request.Address.Latitude,
            Description = request.Address.Description,
        };
        var addressRespons = await _sender.SendAsync(new CreateAddres(address));
        if (addressRespons.StatusCode != 200)
        {
            throw new BadRequestException(" فشلت العمليه ");
        }
        var center = new Centers()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            AddressId = (Guid)addressRespons.Id
        };
        var create = await _centersContext.CreateAsync(center);
        if (create.StatusCode != 200)
        {
            var delete = await _addressContext.DeleteAddressByIdAsync((Guid)addressRespons.Id);
            if (delete.StatusCode != 200)
            {
                throw new BadRequestException(" فشلت العمليه ");
            }
            throw new BadRequestException(" فشلت العمليه ");
        }
        throw new SuccessException(center.Id);
    }
}