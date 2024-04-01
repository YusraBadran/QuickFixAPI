using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.Shared.Images.Models;

namespace QuickFix.Shared.Images.Data.EntityConfigurations
{
    public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.HadImage).IsRequired();
            builder.Property(c => c.Url).IsRequired();

        }
    }
}
