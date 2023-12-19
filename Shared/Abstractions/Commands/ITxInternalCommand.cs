using QuickFix.Shared.Abstractions.Persistence;

namespace QuickFix.Shared.Abstractions.Commands;

public interface ITxInternalCommand : IInternalCommand, ITxRequest { }
