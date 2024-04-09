using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.Settings.Screens.Model;

namespace QuickFix.Settings.Screens.Data.EntityConfiguration
{
    public class ScreenRoleConfiguration : IEntityTypeConfiguration<UserScreen>
    {
        public void Configure(EntityTypeBuilder<UserScreen> builder)
        {
            builder.ToTable("ScreenRole");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Menu).IsRequired();
            builder.Property(m => m.IsView).IsRequired();
            builder.Property(m => m.IsCreated).IsRequired(false);
            builder.Property(m => m.IsDeleted).IsRequired(false);
            builder.Property(m => m.IsDetail).IsRequired(false);
            builder.Property(m => m.IsUpdated).IsRequired(false);
            builder.Property(m => m.IsPrint).IsRequired(false);
            builder.Property(m => m.IsExport).IsRequired(false);
            builder.Property(m => m.IsImport).IsRequired(false);
        }
    }
}
