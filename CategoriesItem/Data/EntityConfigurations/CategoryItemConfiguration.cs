using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.CategoriesItem.Models;
using QuickFix.Shared.Module;

namespace QuickFix.CategoriesItem.Data.EntityConfigurations
{
    public class CategoryItemConfiguration : IEntityTypeConfiguration<CategoryItems>
    {
        public void Configure(EntityTypeBuilder<CategoryItems> builder)
        {
            builder.ToTable("CategoryItem");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Logo).IsRequired(false);
            builder.Property(c => c.Description).HasMaxLength(350).IsRequired();

            builder.Property(c => c.Status).HasDefaultValue(TypeStates.Active);
            builder.Property(c => c.Price).IsRequired();
            builder.Property(c => c.CategoryId).IsRequired(false);

        }
    }
}
