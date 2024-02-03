using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.Categories.Models;
using QuickFix.ServicesType.Models;

namespace QuickFix.Categories.Data.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(350).IsRequired();
            builder.Property(c => c.State).HasDefaultValue(CategoryState.Active);
        }
    }
}
