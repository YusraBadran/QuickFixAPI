using QuickFix.Shared.Abstractions.Commands;
using QuickFix.Shared.Core.Types;

namespace QuickFix.Shared.Core.Commands;

public abstract record InternalCommand : IInternalCommand
{
    public Guid InternalCommandId { get; protected set; } = Guid.NewGuid();

    public DateTime OccurredOn { get; protected set; } = DateTime.Now;

    public string Type
    {
        get { return TypeMapper.GetFullTypeName(GetType()); }
    }
}
