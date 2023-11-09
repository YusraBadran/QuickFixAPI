using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickFix.Identity.Shared.Models;

namespace QuickFix.Identity.Identitys.Data.EntityConfigurations
{
    public class EmailVerificationCodeConfiguration : IEntityTypeConfiguration<EmailVerificationCode>
    {
        public void Configure(EntityTypeBuilder<EmailVerificationCode> builder)
        {
            builder.ToTable("EmailVerificationCodes");
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
               builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Code).HasMaxLength(6).IsFixedLength().IsRequired();
            builder.Property(x=>x.SentAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
            builder.Property(x=>x.UsedAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);
        }
    }
}
