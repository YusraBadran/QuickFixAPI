using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.Categories.Models;
using QuickFix.ServicesType.Models;
using QuickFix.Shared.Module;

namespace QuickFix.Categories.Data.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(350).IsRequired();


            builder.Property(c => c.State).HasDefaultValue(TypeStates.Active);
            builder.Property(c => c.ServiceId).IsRequired(false);
            builder.Property(c => c.SubCategoryId).IsRequired(false);
            builder.HasMany(c => c.CategoryItems).WithOne(s => s.Category).HasForeignKey(s => s.CategoryId).IsRequired(false);
        }
    }
}
