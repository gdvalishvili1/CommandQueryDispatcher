using CommandQueryDispatcher.Common;
using CommandQueryDispatcher.Query.Abstractions;
using System;

namespace CommandQueryDispatcher.Query.Dispatching
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly DependencyResolver _serviceFactory;

        public QueryExecutor(DependencyResolver serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public TResult Execute<TResult>(IQuery<TResult> query) where TResult : IQueryResult
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var requestType = query.GetType();

            var responseType = typeof(TResult);

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(requestType, responseType);

            var handlerInstance = _serviceFactory(handlerType) as dynamic;

            return handlerInstance.Handle(query as dynamic);
        }
    }
}
