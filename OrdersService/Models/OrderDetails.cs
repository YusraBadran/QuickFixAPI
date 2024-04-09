using QuickFix.CategoriesItem.Models;


namespace QuickFix.OrdersService.Models;

public class OrderDetails
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid CategoryItemId { get; set; }
    public string Note { get; set; }
    public virtual Orders Orders { get; set; }
    public virtual CategoryItems CategoryItems { get; set; }
}
