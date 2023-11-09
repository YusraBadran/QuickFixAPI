// using System;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using QuickFix.Identity.Shared.Models;

// namespace QuickFix.Identity.Identitys.Data.EntityConfigurations
// {
//     public class AccessTokenConfiguration:IEntityTypeConfiguration<AccessToken>
//     {
//         public void Configure(EntityTypeBuilder<AccessToken> builder)
//         {
//             builder.ToTable("AccessTokens");
//             // builder.Property<Guid>("InternalCommandId").ValueGeneratedOnAdd();
//             // builder.HasKey("InternalCommandId");
//             builder.HasIndex(x=> new {x.Token,x.UserId}).IsUnique();
//             builder.HasOne(rt =>rt.ApplicationUser).WithMany(au=>au.AccessTokens).HasForeignKey(x=>x.UserId);
//             builder.Property(rt=>rt.Token);
//             builder.Property(rt=>rt.CreatedAt);
//         }
//     }

// }
