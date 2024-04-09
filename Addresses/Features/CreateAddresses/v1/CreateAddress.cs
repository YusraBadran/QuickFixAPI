using QuickFix.Addresses.Models;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using QuickFix.Shared.Module;
using FluentValidation;
using QuickFix.Addresses.Data;
using QuickFix.Addresses.Extensions;

namespace QuickFix.Addresses.Features.CreateAddresses.v1;

public record CreateAddres : CreateAddresRequest, ITxCreateCommand<DataRespons>
{
    public CreateAddres(CreateAddresRequest request) : base(request) { }

}
public class Validate : AbstractValidator<CreateAddres>
{
    public Validate()
    {
        RuleFor(v => v.Location).NotEmpty().WithMessage(" الموقع مطلوب ");
        RuleFor(v => v.Latitude).NotEmpty().WithMessage(" خط العرض مطلوب ");
        RuleFor(v => v.Longitude).NotEmpty().WithMessage(" خط الطول مطلوب ");
        //RuleFor(v => v.description).MaximumLength(250).WithMessage(" الوصف لا يزيد عن 250 حرف ");
    }
}
public class CreateAddressHandler : ICommandHandler<CreateAddres, DataRespons>
{
    private readonly IAddressDbContext _addressContext;
    public CreateAddressHandler(IAddressDbContext addressContext)
    {
        _addressContext = addressContext;
    }

    public async Task<DataRespons> Handle(CreateAddres request, CancellationToken cancellationToken)
    {
        var address = new AddressModel()
        {
            Id = Guid.NewGuid(),
            Location = request.Location,
            Longitude = request.Longitude,
            Latitude = request.Latitude,
            description = request.Description,
        };
        var addressRespons = await _addressContext.CreateAddressAsync(address, cancellationToken);
        return addressRespons;
    }
}