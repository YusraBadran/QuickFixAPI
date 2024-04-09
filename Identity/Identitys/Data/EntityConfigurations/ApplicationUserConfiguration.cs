using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.Identity.Shared.Models;

using QuickFix.Shared.Core.Persistence.EfCore;
using QuickFix.Shared.Module;

namespace QuickFix.Identity.Identitys.Data.EntityConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.NormalizedUserName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.NormalizedEmail).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql(EfConstants.DateAlgorithm);
            builder.Property(x => x.UserState).HasDefaultValue(TypeStates.Inactive)
            .HasConversion(x => x.ToString(),
            x => (TypeStates)Enum.Parse(typeof(TypeStates), x));
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.NormalizedEmail).IsUnique();
            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(ur => ur.UserId).IsRequired();

        }
    }
}