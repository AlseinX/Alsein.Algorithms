using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Alsein.Algorithms.Searching.SearchAlgorithms
{
    public class BreadthFirstSearchAlgorithm : SearchAlgorithm
    {
        internal override IEnumerable<ISearchContext<TData>> GetContextSupplier<TData>(IEnumerable<TData> initials) => new SearchProvider<TData, ContextPool<TData>>(initials);

        private class ContextPool<TData> : IContextPool<TData> where TData : class
        {
            private ConcurrentQueue<ISearchContext<TData>> Data;

            public ContextPool() => Data = new ConcurrentQueue<ISearchContext<TData>>();

            public void Add(ISearchContext<TData> context) => Data.Enqueue(context);

            public bool Contains(ISearchContext<TData> context) => Data.Contains(context);

            public bool TryFetch(out ISearchContext<TData> context) => Data.TryDequeue(out context);
        }
    }
}