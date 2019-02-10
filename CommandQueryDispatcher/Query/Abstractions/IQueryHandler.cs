using System;
using System.Collections.Generic;
using System.Text;

namespace CommandQueryDispatcher.Query.Abstractions
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
