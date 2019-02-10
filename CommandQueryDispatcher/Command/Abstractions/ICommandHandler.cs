using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueryDispatcher.Command.Abstractions
{
    public interface ICommandHandler<in TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
        TResult Handle(TCommand command);
    }
}
