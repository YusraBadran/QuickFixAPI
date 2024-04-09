
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.OrdersService.Models;
using QuickFix.Shared.Core.Persistence.EfCore;
using QuickFix.Shared.Module;

namespace QuickFix.OrdersService.Data.EntityConfigurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.OrderNumber).IsRequired();
            builder.Property(p => p.Status).HasDefaultValue(TypeStates.WaitingList).IsRequired();
            builder.Property(p => p.TotalPrice).HasColumnType(EfConstants.ColumnTypes.PriceDecimal).IsRequired();
            builder.Property(p => p.Note).IsRequired();
            builder.Property(p => p.Phone).IsRequired(false);
            builder.Property(p => p.Date).HasDefaultValueSql(EfConstants.DateAlgorithm).IsRequired();
            builder.Property(p => p.PeriodByDay).HasDefaultValueSql(EfConstants.DateAlgorithm).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.AddressId).IsRequired();
            builder.HasMany(p => p.OrderDetails).WithOne(p => p.Orders).HasForeignKey(p => p.OrderId).IsRequired();

        }
    }
}
