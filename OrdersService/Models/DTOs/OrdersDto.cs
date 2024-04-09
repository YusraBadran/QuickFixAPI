using QuickFix.Identity.Shared.Models;
using QuickFix.Shared.Module;

namespace QuickFix.OrdersService.Models.DTOs;

public class OrdersDto
{
    public Guid Id { get; set; }
    public TypeStates Status { get; set; }
    public double TotalPrice { get; set; }
    public string Note { get; set; }
    public DateTime Date { get; set; }
    public DateTime PeriodByDay { get; set; }
    public string Phone { get; set; }
    public string FullNameUser { get; set; }
}
