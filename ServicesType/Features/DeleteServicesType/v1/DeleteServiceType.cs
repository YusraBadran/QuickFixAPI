using FluentValidation;
using QuickFix.ServicesType.Data;
using QuickFix.ServicesType.Exceptions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Features.DeleteServicesType.v1;

public record DeleteServiceType(Guid Id) : ITxCommand<DataRespons>
{
}
public class Validator : AbstractValidator<DeleteServiceType>
{
    public Validator()
    {
        RuleFor(d => d.Id).NotEmpty().WithMessage(" المعرف مطلوب");

    }
}
public class DeleteServiceTypeHandler : ICommandHandler<DeleteServiceType, DataRespons>
{
    private readonly IServiceTypeContext _context;
    public DeleteServiceTypeHandler(IServiceTypeContext context)
    {
        _context = context;
    }
    public async Task<DataRespons> Handle(DeleteServiceType request, CancellationToken cancellationToken)
    {
        var serviceType = await _context.FindServiceTypeById(request.Id);
        if (serviceType == null)
        {
            throw new ServiceTypeNotFoundException();
        }
        var respons = await _context.DeleteAsync(serviceType);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        throw new SuccessException(request.Id);
    }
}
