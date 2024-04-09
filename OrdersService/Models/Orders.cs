using QuickFix.Addresses.Models;
using QuickFix.Identity.Shared.Models;
using QuickFix.Shared.Module;

namespace QuickFix.OrdersService.Models;

public class Orders
{
    public Guid Id { get; set; }
    public TypeStates Status { get; set; }
    public int OrderNumber { get; set; }
    public double TotalPrice { get; set; }
    public string? Note { get; set; }
    public string? Phone { get; set; }
    public DateTime Date { get; set; }
    public DateTime? PeriodByDay { get; set; }
    public Guid UserId { get; set; }
    public Guid AddressId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual AddressModel Address { get; set; }
    public virtual ICollection<OrderDetails> OrderDetails { get; set; }

}
