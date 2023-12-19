using QuickFix.Shared.Abstractions.Persistence;
using MediatR;

namespace QuickFix.Shared.Abstractions.Commands;

public interface ITxCreateCommand<out TResponse> : ICommand<TResponse>, ITxRequest
    where TResponse : notnull
{ }

public interface ITxCreateCommand : ITxCreateCommand<Unit> { }
