
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.Addresses.Models;
using QuickFix.MaintenanceCenters.Module;
using QuickFix.OrdersService.Models;
namespace QuickFix.Addresses.Data.EntityConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<AddressModel>
    {
        public void Configure(EntityTypeBuilder<AddressModel> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Location).IsRequired();
            builder.Property(a => a.Longitude).IsRequired();
            builder.Property(a => a.Latitude).IsRequired();
            builder.Property(a => a.description).IsRequired(false);
            builder.HasOne(p => p.Orders).WithOne(p => p.Address).HasForeignKey<Orders>(p => p.AddressId).IsRequired();
            builder.HasOne(a => a.Centers).WithOne(a => a.Address).HasForeignKey<Centers>(f => f.AddressId).IsRequired();

        }
    }
}
