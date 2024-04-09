namespace QuickFix.OrdersService.Models.DTOs;

public class OrderDetailsDtos
{
    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Note { get; set; }
}
