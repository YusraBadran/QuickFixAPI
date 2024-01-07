using  QuickFix.Shared.Abstractions.Commands;

using MediatR;

namespace  QuickFix.Shared.Core.Commands;

public class CommandProcessor : ICommandProcessor
{
    private readonly ISender _mediator;


    public CommandProcessor(ISender mediator)
    {
        _mediator = mediator;

    }

    public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        where TResult : notnull
    {
        return _mediator.Send(command, cancellationToken);
    }

}
