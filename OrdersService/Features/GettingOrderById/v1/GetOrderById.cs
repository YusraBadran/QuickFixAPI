using AutoMapper;
using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using QuickFix.OrdersService.Data;
using QuickFix.OrdersService.Extensions;
using QuickFix.OrdersService.Models.DTOs;

namespace QuickFix.OrdersService.Features.GettingOrderById.v1;

public record GetOrderById(Guid Id) : ICommand<GetOrderByIdRespons>;
public class Validator : AbstractValidator<GetOrderById>
{
    public Validator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(" يجب ارسال المعرف. ");
    }
}
public class GetOrderByIdHandler : ICommandHandler<GetOrderById, GetOrderByIdRespons>
{
    private readonly IMapper _mapper;
    private readonly IOrdersServiceDbContext _orderContexct;

    public GetOrderByIdHandler(IMapper mapper, IOrdersServiceDbContext orderContext)
    {
        _mapper = mapper;
        _orderContexct = orderContext;

    }

    public async Task<GetOrderByIdRespons> Handle(GetOrderById request, CancellationToken cancellationToken)
    {
        var order = await _orderContexct.FindOrdersById(request.Id);
        var orderDTOs = _mapper.Map<OrderDto>(order);
        return new GetOrderByIdRespons(orderDTOs);
    }
}
