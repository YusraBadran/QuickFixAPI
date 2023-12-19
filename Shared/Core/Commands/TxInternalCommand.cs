using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Shared.Core.Commands;

public abstract record TxInternalCommand : InternalCommand, ITxInternalCommand;
