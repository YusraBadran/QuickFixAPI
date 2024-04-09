using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Module;

namespace QuickFix.ServicesType.Data.EntityConfigurations
{
    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceType");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Logo).IsRequired(false);

            builder.Property(x => x.Description).HasMaxLength(350);

            builder.Property(x => x.Status).HasDefaultValue(TypeStates.Inactive);
            builder.HasMany(s => s.Categories).WithOne(c => c.ServiceType).HasForeignKey(c => c.ServiceId);

        }
    }
}
