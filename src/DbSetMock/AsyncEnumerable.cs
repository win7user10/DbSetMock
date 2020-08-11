﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace DbSetMock
{
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public AsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }

        public AsyncEnumerable(Expression expression) : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncEnumerator<T>(this.AsQueryable().AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);
    }
}
