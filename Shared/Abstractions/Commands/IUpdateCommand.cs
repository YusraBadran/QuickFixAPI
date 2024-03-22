namespace QuickFix.Shared.Abstractions.Commands;
using MediatR;

public interface IUpdateCommand : IUpdateCommand<Unit> { }

public interface IUpdateCommand<out TResponse> : ICommand<TResponse>
    where TResponse : notnull
{ }
