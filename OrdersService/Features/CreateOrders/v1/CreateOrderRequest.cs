using QuickFix.Addresses.Models;
using QuickFix.Addresses.Models.DTOs;
using QuickFix.Shared.Module;

namespace QuickFix.OrdersService.Features.CreateOrders.v1
{
    public record CreateOrderRequest
    {
        public TypeStates Status { get; set; }
        public double TotalPrice { get; set; }
        public string Note { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public virtual AddressDTOs Address { get; set; }
        public IEnumerable<CreateOrderDetailsRequest> Details { get; set; }
    }
    public record CreateOrderDetailsRequest
    {
        public Guid CategoryItemId { get; set; }
        public string? Note { get; set; }
    }
}
