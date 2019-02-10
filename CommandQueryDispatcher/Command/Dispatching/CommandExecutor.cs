using CommandQueryDispatcher.Command.Abstractions;
using CommandQueryDispatcher.Common;
using System;

namespace CommandQueryDispatcher.Command.Dispatching
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly DependencyResolver _serviceFactory;

        public CommandExecutor(DependencyResolver serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public TResult Execute<TResult>(ICommand<TResult> command) where TResult : ICommandResult
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var requestType = command.GetType();

            var responseType = typeof(TResult);

            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(requestType, responseType);

            var handlerInstance = _serviceFactory(handlerType) as dynamic;

            return handlerInstance.Handle(command as dynamic);
        }
    }
}
