using MediatR;
using To_Do.SharedKernel.Result;

namespace To_Do.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IBaseCommand
{
}
