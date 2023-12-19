namespace QuickFix.Shared.Abstractions.Types;

public interface IMachineInstanceInfo
{
    string ClientGroup { get; }
    Guid ClientId { get; }
}
