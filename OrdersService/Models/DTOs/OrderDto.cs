using QuickFix.Addresses.Models;
using QuickFix.Addresses.Models.DTOs;
using QuickFix.Identity.Shared.Models;
using QuickFix.Identity.Users.Models.DTOS.v1;
using QuickFix.Shared.Module;

namespace QuickFix.OrdersService.Models.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }
    public TypeStates Status { get; set; }
    public int OrderNumber { get; set; }
    public double TotalPrice { get; set; }
    public string Note { get; set; }
    public string Phone { get; set; }
    public DateTime Date { get; set; }
    public DateTime PeriodByDay { get; set; }
    public virtual OrderUserDto User { get; set; }
    public virtual AddressDTOs Address { get; set; }
    public virtual IEnumerable<OrderDetailsDtos> OrderDetails { get; set; }
}
