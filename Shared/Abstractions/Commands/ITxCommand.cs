using QuickFix.Shared.Abstractions.Persistence;
using MediatR;

namespace QuickFix.Shared.Abstractions.Commands;

public interface ITxCommand : ITxCommand<Unit> { }

public interface ITxCommand<out T> : ICommand<T>, ITxRequest
    where T : notnull
{ }
