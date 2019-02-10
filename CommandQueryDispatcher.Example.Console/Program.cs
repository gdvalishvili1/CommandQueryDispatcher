using CommandQueryDispatcher.Command.Abstractions;
using CommandQueryDispatcher.Command.Dispatching;
using CommandQueryDispatcher.Common;
using CommandQueryDispatcher.Query.Abstractions;
using CommandQueryDispatcher.Query.Dispatching;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CommandQueryDispatcher.Example.Console
{
    public class ChangeNameCommand : IDefaultCommand
    {
        public string Name { get; set; }
    }

    public class ChangeNameCommandHandler : IDefaultCommandHandler<ChangeNameCommand>
    {
        public CommandExecutionResult Handle(ChangeNameCommand command)
        {
            return new CommandExecutionResult
            {
                Success = true,
                Data = new
                {
                    Name = "test"
                }
            };
        }
    }

    public class LoggableCommandExecutor
    {
        private readonly ICommandExecutor _executor;

        public LoggableCommandExecutor(ICommandExecutor executor)
        {
            _executor = executor;
        }
        public CommandExecutionResult Execute(dynamic command)
        {
            try
            {
                return _executor.Execute(command);
            }
            catch (Exception ex)
            {
                return new CommandExecutionResult
                {
                    Success = false,
                    Data = ex.ToString()
                };
            }

        }
    }

    //Query

    public class NamesQuery : IQuery<NamesQueryResult>
    {
        public int Id { get; set; }
    }

    public class NamesQueryResult : IQueryResult
    {
        public List<string> Names { get; set; }
    }

    public class NamesQueryHandler : IQueryHandler<NamesQuery, NamesQueryResult>
    {
        public NamesQueryResult Handle(NamesQuery query)
        {
            return new NamesQueryResult
            {
                Names = new List<string>
                {
                    "names1",
                    "names2"
                }
            };
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var provider = GetResolverCase2();

            var queryResult = provider.GetService<IQueryExecutor>()
                .Execute(new NamesQuery
                {
                    Id = 1
                });


            //use case 1
            var resultOnHandler = new ChangeNameCommandHandler()
                .Handle(new ChangeNameCommand
                {
                    Name = "newName"
                });


            //use case 2 
            var executor2 = provider.GetService<ICommandExecutor>();

            var resultOnCommandExecutor = executor2.Execute(new ChangeNameCommand { Name = "newName" });


            //use case 3 
            var provider3 = GetResolverCase3();

            var loggableExecutor = provider3.GetService<LoggableCommandExecutor>();

            var resultOnLoggable = loggableExecutor.Execute(new ChangeNameCommand { Name = "newName" });
        }

        private static ServiceProvider GetResolverCase2()
        {
            var services = new ServiceCollection();

            services.AddScoped<DependencyResolver>(p => p.GetService);
            services.AddScoped<ICommandExecutor, CommandExecutor>();
            services.AddScoped<IQueryExecutor, QueryExecutor>();

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ICommandExecutor), typeof(ChangeNameCommandHandler))
                .AddClasses()
                .AsImplementedInterfaces());

            var provider = services.BuildServiceProvider();
            return provider;
        }


        private static ServiceProvider GetResolverCase3()
        {
            var services = new ServiceCollection();

            services.AddScoped<DependencyResolver>(p => p.GetService);
            services.AddScoped<ICommandExecutor, CommandExecutor>();
            services.AddScoped<LoggableCommandExecutor>();

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ICommandExecutor), typeof(ChangeNameCommandHandler))
                .AddClasses()
                .AsImplementedInterfaces());

            var provider = services.BuildServiceProvider();
            return provider;
        }

    }
}
