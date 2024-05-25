using Ardalis.GuardClauses;
using QuickFix.Shared.Exceptions;
using QuickFix.Identity.Identitys.Exceptions;
using QuickFix.Identity.Shared.Exceptions;
using QuickFix.Shared.Exceptions.Types;

using FluentValidation;

using Microsoft.AspNetCore.Identity;
using QuickFix.Identity.Shared.Models;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Module;
using MediatR;
using QuickFix.OrdersService.Exceptions;
using QuickFix.Settings.Notifications.Interface;
using QuickFix.Settings.Notifications.Models;
using Microsoft.AspNetCore.SignalR;
using QuickFix.OrdersService.Data;
using QuickFix.OrdersService.Extensions;

namespace QuickFix.OrdersService.Features.UpdateOrdersState.v1;

public record UpdateOrderState : UpdateOrderStateRequest, ITxUpdateCommand<DataRespons>
{
    public UpdateOrderState(UpdateOrderStateRequest request) : base(request)
    { }

    public class Validator : AbstractValidator<UpdateOrderState>
    {
        public Validator()
        {

            RuleFor(v => v.Id).NotEmpty().WithMessage(" يجب ارسال رقم الطلب. ");

            RuleFor(v => v.status).NotEmpty().WithMessage(" يجب ارسال حالة الطلب. ");
        }
    }

    internal class UpdateOrderStateHandler : ICommandHandler<UpdateOrderState, DataRespons>
    {
        private readonly ILogger<UpdateOrderStateHandler> _logger;
        private readonly IOrdersServiceDbContext _orderContext;
        private readonly IHubContext<NotificationHub, INotificationHub> _notify;

        public UpdateOrderStateHandler(
            IOrdersServiceDbContext orderContext,
            ILogger<UpdateOrderStateHandler> logger,
             IHubContext<NotificationHub, INotificationHub> notify
        )
        {
            _logger = logger;
            _orderContext = orderContext;
            _notify = notify;
        }

        public async Task<DataRespons> Handle(UpdateOrderState request, CancellationToken cancellationToken)
        {
            var order = await _orderContext.FindOrdersById(request.Id);
            if (order == null)
            {
                throw new NotFoundOrderWithIdException(request.Id);
            }
            order.Status = request.status;
            var updateOrder = await _orderContext.UpdateAsync(order, cancellationToken);
            if (updateOrder.StatusCode != 200)
            {
                throw new BadRequestException(updateOrder.Message);
            }
            if (request.status == TypeStates.Accepted)
            {
                await _notify.Clients.User(order.UserId.ToString()).SendNotificationAsync(" تم قبول طلبك ");
            }
            else
            {
                await _notify.Clients.User(order.UserId.ToString()).SendNotificationAsync(" تم رفض طلبك ");
            }
            throw new SuccessException(order.Id);
        }
    }
}
