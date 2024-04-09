using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using QuickFix.Addresses.Data;
using QuickFix.Addresses.Extensions;
using QuickFix.Addresses.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Addresses.Features.GetAddressByUsersId.v1;

public record GetAddressByUserId(Guid Id) : ICommand<GetAddressByUserIdRespons>
{
}
public class GetAddressByUserIdHandler : ICommandHandler<GetAddressByUserId, GetAddressByUserIdRespons>
{
    private readonly IMapper _mapper;
    private readonly IAddressDbContext _addressContext;

    public GetAddressByUserIdHandler(IMapper mapper, IAddressDbContext addressContext)
    {
        _mapper = mapper;
        _addressContext = addressContext;
    }
    public async Task<GetAddressByUserIdRespons> Handle(GetAddressByUserId request, CancellationToken cancellationToken)
    {
        var data = await _addressContext.FindAddressByUserIdAsync(request.Id);
        var result = _mapper.Map<AddressDTOs>(data);
        return new GetAddressByUserIdRespons(result);
    }
}
