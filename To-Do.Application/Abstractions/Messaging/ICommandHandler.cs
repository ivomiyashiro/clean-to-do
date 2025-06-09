using To_Do.SharedKernel.Result;

namespace To_Do.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand>
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}