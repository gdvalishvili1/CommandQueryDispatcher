using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueryDispatcher.Command.Abstractions
{
    public interface ICommandExecutor
    {
        TResult Execute<TResult>(ICommand<TResult> command) where TResult : ICommandResult;
    }
}
