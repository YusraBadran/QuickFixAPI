using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.ServicesType.Models;

namespace QuickFix.ServicesType.Data.EntityConfigurations
{
    public class ServiceTypeConfiguration: IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceType");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.NameEn).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.Description).HasMaxLength(350);
            builder.Property(x=>x.DescriptionEn).HasMaxLength(350);
            builder.Property(x=>x.Status).HasDefaultValue(ServicesTypeState.Active);

        }
    }
}
