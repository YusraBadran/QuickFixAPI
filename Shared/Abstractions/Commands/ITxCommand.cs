using QuickFix.Shared.Abstractions.Persistence;
using MediatR;
using QuickFix.Shared.Abstractions.Persistence;

namespace QuickFix.Shared.Abstractions.Commands;

public interface ITxCommand : ITxCommand<Unit> { }

public interface ITxCommand<out T> : ICommand<T>, ITxRequest
    where T : notnull
{ }
