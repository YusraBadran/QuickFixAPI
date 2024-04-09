using FluentValidation;
using QuickFix.Categories.Data;
using QuickFix.Categories.Exceptions;
using QuickFix.Categories.Extensions;
using QuickFix.CategoriesItem.Data;
using QuickFix.CategoriesItem.Extensions;
using QuickFix.MaintenanceCenters.Data;
using QuickFix.MaintenanceCenters.Extensions;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;

namespace QuickFix.MaintenanceCenters.Features.DeletCenters.v1;

public record DeletCenterss(Guid Id) : ITxCommand<DataRespons>;
public class Validator : AbstractValidator<DeletCenterss>
{
    public Validator()
    {
        RuleFor(c => c.Id).NotEmpty().NotNull().WithMessage("يجب تحديد الفائه");
    }
}
public class DeleteDeletCenterssHandler : ICommandHandler<DeletCenterss, DataRespons>
{
    private readonly ICentersDbContext _center;
    public DeleteDeletCenterssHandler(ICentersDbContext center)
    {
        _center = center;
    }
    public async Task<DataRespons> Handle(DeletCenterss request, CancellationToken cancellationToken)
    {
        var center = await _center.FindCentersById(request.Id);
        if (center == null)
        {
            throw new CategoryNotFoundException();
        }
        var respons = await _center.DeleteAsync(center);
        if (respons.StatusCode != 200)
        {
            throw new BadRequestException(respons.Message);
        }
        throw new SuccessException(center.Id);
    }
}