using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.OrdersService.Models;

namespace QuickFix.OrdersService.Data.EntityConfigurations
{
    public class OrdersDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Note).IsRequired(false);
        }
    }
}
