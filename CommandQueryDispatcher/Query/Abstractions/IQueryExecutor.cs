using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueryDispatcher.Query.Abstractions
{
    public interface IQueryExecutor
    {
        TResult Execute<TResult>(IQuery<TResult> query) where TResult : IQueryResult;
    }
}
