using System.Data.Common;

namespace QuickFix.Shared.Abstractions.Persistence.EfCore;

public interface IConnectionFactory : IDisposable
{
    Task<DbConnection> GetOrCreateConnectionAsync();
}
