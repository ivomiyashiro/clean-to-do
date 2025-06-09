namespace To_Do.Application.Abstractions.Messaging;

public interface ICommand
{
}

public interface ICommand<TReponse> : IBaseCommand
{
}

public interface IBaseCommand
{
}
