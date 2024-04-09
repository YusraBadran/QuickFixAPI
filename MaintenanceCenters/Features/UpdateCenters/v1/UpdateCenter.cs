using FluentValidation;

using QuickFix.MaintenanceCenters.Data;
using QuickFix.MaintenanceCenters.Exceptions;
using QuickFix.MaintenanceCenters.Extensions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.MaintenanceCenters.Features.UpdateCenters.v1;

public record UpdateCenter : UpdateCenterRequest, ITxCreateCommand<DataRespons>
{
    public UpdateCenter(UpdateCenterRequest request) : base(request) { }
}
public class Validator : AbstractValidator<UpdateCenter>
{
    public Validator()
    {
        RuleFor(v => v.Name).NotEmpty().WithMessage(" الاسم مطلوب ");
        RuleFor(v => v.Description).NotEmpty().WithMessage(" الوصف مطلوب ");
        RuleFor(v => v.Address.Longitude).NotEmpty().WithMessage(" خط الطول مطلوب ");
        RuleFor(v => v.Address.Latitude).NotEmpty().WithMessage(" خط العرض مطلوب ");
    }
}
public class CreateCategoryHandler : ICommandHandler<UpdateCenter, DataRespons>
{
    private readonly ICentersDbContext _context;
    public CreateCategoryHandler(ICentersDbContext context)
    {
        _context = context;
    }
    public async Task<DataRespons> Handle(UpdateCenter request, CancellationToken cancellationToken)
    {
        var center = await _context.FindCentersById(request.Id);
        var nameEx = await _context.FindCenterByName(request.Name);
        if (nameEx != null && nameEx.Id != center.Id)
        {
            throw new CenterNameAlreadyExistException(request.Name);
        }

        center.Name = request.Name;
        center.Description = request.Description;
        center.Status = request.Status;
        var respons = await _context.UpdateAsync(center);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        throw new SuccessException(center.Id);
    }
}