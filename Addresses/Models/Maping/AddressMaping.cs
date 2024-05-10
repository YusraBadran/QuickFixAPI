using AutoMapper;
using QuickFix.Addresses.Models;
using QuickFix.Addresses.Models.DTOs;

namespace QuickFix.Addresses.Models.Maping
{
    public class AddressMaping : Profile
    {
        public AddressMaping()
        {
            CreateMap<AddressModel, AddressDTOs>();
            CreateMap<AddressModel, AddressDto>();
        }
    }
}
