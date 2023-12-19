using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Reflection.Emit;
using QuickFix.Identity.Identitys.Data.EntityConfigurations;
using QuickFix.Identity.Shared.Models;


namespace QuickFix.DbContexts
{
    public class AppDbContext : IdentityDbContext<
          ApplicationUser,
        ApplicationRole,
        Guid,
        IdentityUserClaim<Guid>,
        ApplicationUserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>
        >
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }
 protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder.ApplyConfiguration(new AccessTokenConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new EmailVerificationCodeConfiguration());
            builder.ApplyConfiguration(new PasswordResetCodeConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // https://andrewlock.net/customising-asp-net-core-identity-ef-core-naming-conventions-for-postgresql/
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.SetTableName(entity.GetTableName()?.Underscore());

                var ecommerceObjectIdentifier = StoreObjectIdentifier.Table(
                    entity.GetTableName()?.Underscore()!,
                    entity.GetSchema()
                );

                // Replace column names
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName(ecommerceObjectIdentifier)?.Underscore());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName()?.Underscore());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName()?.Underscore());
                }
            }
        }

        public Task ExecuteTransactionalAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            var strategy = Database.CreateExecutionStrategy();
            return strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await Database.BeginTransactionAsync(
                    IsolationLevel.ReadCommitted,
                    cancellationToken
                );
                try
                {
                    await action();

                    await transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }

        public Task<T> ExecuteTransactionalAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
        {
            var strategy = Database.CreateExecutionStrategy();
            return strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await Database.BeginTransactionAsync(
                    IsolationLevel.ReadCommitted,
                    cancellationToken
                );
                try
                {
                    var result = await action();

                    await transaction.CommitAsync(cancellationToken);

                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }
        public void MarkUncommittedDomainEventAsCommitted()
        {
            // Method intentionally left empty.
        }
    }
}