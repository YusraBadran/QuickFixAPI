
using QuickFix.Shared.Abstractions.Persistence;
using MediatR;

namespace QuickFix.Shared.Abstractions.Commands;

public interface ITxUpdateCommand<out TResponse> : IUpdateCommand<TResponse>, ITxRequest
    where TResponse : notnull
{ }

public interface ITxUpdateCommand : ITxUpdateCommand<Unit> { }
