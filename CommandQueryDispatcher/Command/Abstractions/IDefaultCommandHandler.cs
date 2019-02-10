using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueryDispatcher.Command.Abstractions
{
    public interface IDefaultCommandHandler<TCommand> :
        ICommandHandler<TCommand, CommandExecutionResult> where TCommand : IDefaultCommand
    {

    }
}
