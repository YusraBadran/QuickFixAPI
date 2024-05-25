using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.CategoriesItem.Models;
using QuickFix.MaintenanceCenters.Module;
using QuickFix.Shared.Module;

namespace QuickFix.MaintenanceCenters.Data.EntityConfigurations
{
    public class CentersConfiguration : IEntityTypeConfiguration<Centers>
    {
        public void Configure(EntityTypeBuilder<Centers> builder)
        {
            builder.ToTable("Centers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Phone).HasMaxLength(9).IsRequired();
            builder.Property(c => c.Status).HasDefaultValue(TypeStates.Active);
            builder.Property(c => c.Description).IsRequired(false);
        }
    }
}
