using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.Settings.Screens.Model;

namespace QuickFix.Settings.Screens.Data.EntityConfiguration
{
    public class ScreenConfiguration : IEntityTypeConfiguration<ScreenModel>
    {
        public void Configure(EntityTypeBuilder<ScreenModel> builder)
        {
            builder.ToTable("Screens");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.HashName).IsRequired(false);
            builder.HasIndex(m => m.HashName).IsUnique();
            builder.Property(m => m.Label).IsRequired();
            builder.Property(m => m.order);
            builder.Property(m => m.Icon).IsRequired(false);
            builder.Property(m => m.IconImge).IsRequired(false);
            builder.Property(m => m.Translate).IsRequired();
            builder.Property(m => m.RouterLink).IsRequired(false);
            builder.Property(m => m.SubId).IsRequired(false);
            builder.HasMany(m => m.Role).WithOne(m => m.Screen).HasForeignKey(m => m.ScreenId);
        }
    }
}
