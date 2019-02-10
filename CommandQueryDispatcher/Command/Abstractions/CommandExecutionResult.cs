using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueryDispatcher.Command.Abstractions
{
    public class CommandExecutionResult : ICommandResult
    {
        public bool Success { get; set; }
        public Object Data { get; set; }
    }
}
