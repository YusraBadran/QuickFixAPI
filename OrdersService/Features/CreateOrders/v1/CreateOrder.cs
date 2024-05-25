using AutoMapper;
using QuickFix.Addresses.Data;
using QuickFix.Addresses.Extensions;
using QuickFix.Addresses.Models;
using QuickFix.Identity.Shared.Models;
using QuickFix.OrdersService.Models;
using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Exceptions.Types;
using QuickFix.Shared.Module;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using QuickFix.OrdersService.Data;
using QuickFix.OrdersService.Extensions;
using QuickFix.OrdersService.Models;
using QuickFix.Settings.Notifications.Models;
using QuickFix.Settings.Notifications.Interface;

namespace QuickFix.OrdersService.Features.CreateOrders.v1;

public record CreateOrder : CreateOrderRequest, ICommand<DataRespons>
{
    public CreateOrder(CreateOrderRequest request) : base(request) { }
}

public class Validator : AbstractValidator<CreateOrder>
{
    public Validator()
    {
        RuleFor(v => v.TotalPrice).NotEmpty().WithMessage(" السعر الكلي مطلوب ");
        RuleFor(v => v.Date).NotEmpty().WithMessage(" التاريخ مطلوب ");
        RuleFor(v => v.UserId).NotEmpty().WithMessage(" العميل مطلوب ");
        RuleFor(v => v.Address.Longitude).NotEmpty().WithMessage(" خط الطول مطلوب ");
        RuleFor(v => v.Address.Latitude).NotEmpty().WithMessage(" خط العرض مطلوب ");
        RuleFor(v => v.Address.Location).NotEmpty().WithMessage(" الموقع مطلوب ");
        RuleFor(v => v.Details).NotEmpty().WithMessage(" تفاصيل الطلب مطلوبة ");
    }
}

public class CreateOrderHandler : ICommandHandler<CreateOrder, DataRespons>
{
    private readonly ICommandProcessor _sender;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<CreateOrderHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrdersServiceDbContext _orderContext;
    private readonly IAddressDbContext _addressContext;
    private readonly IHubContext<NotificationHub, INotificationHub> _notify;
    public CreateOrderHandler(
        IOrdersServiceDbContext orderContext, IMapper mapper,
        ICommandProcessor sender, UserManager<ApplicationUser> userManager,
        ILogger<CreateOrderHandler> logger, IAddressDbContext addressContext,
        IHubContext<NotificationHub, INotificationHub> notify)
    {
        _orderContext = orderContext;
        _sender = sender;
        _mapper = mapper;
        _logger = logger;
        _addressContext = addressContext;
        _userManager = userManager;
        _notify = notify;
    }

    public async Task<DataRespons> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var address = new AddressModel
        {
            Id = Guid.NewGuid(),
            Location = request.Address.Location,
            Longitude = request.Address.Longitude,
            Latitude = request.Address.Latitude
        };
        var createdAddress = await _addressContext.CreateAddressAsync(address, cancellationToken);
        if (createdAddress.StatusCode != 200)
        {
            throw new BadRequestException(createdAddress.Message);
        }
        if (string.IsNullOrEmpty(request.Phone))
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            request.Phone = user.PhoneNumber;
        }
        Random rnd = new Random();
        var orderNumber = rnd.Next(100000, 999999);

        var order = new Orders
        {
            Id = Guid.NewGuid(),
            TotalPrice = request.TotalPrice,
            OrderNumber = orderNumber,
            Date = request.Date,
            PeriodByDay = DateTime.Now.AddDays(1),
            UserId = request.UserId,
            AddressId = (Guid)createdAddress.Id,
            Status = TypeStates.WaitingList,
            Phone = request.Phone,
            Note = request.Note,
        };
        var orderDetails = new List<OrderDetails>();
        foreach (var item in request.Details)
        {
            var orderDetail = new OrderDetails
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                CategoryItemId = item.CategoryItemId,
                Note = item.Note
            };
            orderDetails.Add(orderDetail);
        }

        var create = await _orderContext.CreateAsync(order, orderDetails);
        if (create.StatusCode != 200)
        {
            throw new BadRequestException(create.Message);
        }
        var orderDto = await _orderContext.FindOrdersById(order.Id);
        var branchordersDtos = _mapper.Map<NotificationRequest>(orderDto);
        var Id = "Admin";
        await _notify.Clients.Group(Id).SendNotificationAsync(" لديك طلب جديد ");
        throw new SuccessException(order.Id);
    }
}