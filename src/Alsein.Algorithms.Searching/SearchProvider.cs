using System.Collections;
using System.Collections.Generic;
using Alsein.Utilities;

namespace Alsein.Algorithms.Searching
{
    internal class SearchProvider<TData, TContextPool> : IEnumerable<ISearchContext<TData>>
    where TData : class
    where TContextPool : IContextPool<TData>, new()
    {
        private class Enumerator : ISearchEnumerator<TData>
        {
            private readonly IEnumerable<TData> initialData;
            private IContextPool<TData> data;
            private IList<ISearchContext<TData>> removed;
            private bool broken;
            public void Break() => broken = true;
            public ISearchContext<TData> Current { get; private set; }
            object IEnumerator.Current => Current;

            public Enumerator(IEnumerable<TData> initialData)
            {
                this.initialData = initialData;
                Reset();
            }

            public void Dispose() { }

            public bool MoveNext()
            {
                lock (data)
                {
                    while (!broken && data.TryFetch(out var result))
                    {
                        if (removed.Remove(result))
                        {
                            continue;
                        }
                        Current = result;
                        return true;
                    }
                    return false;
                }
            }

            public void Reset()
            {
                lock (this)
                {
                    data = new TContextPool();
                    initialData.ForAll(x => data.Add(new SearchContext<TData>(x, this)));
                    broken = false;
                    removed = new List<ISearchContext<TData>>();
                }
            }

            public void Add(ISearchContext<TData> context) => data.Add(context);

            public bool Remove(ISearchContext<TData> context)
            {
                lock (data)
                {
                    if (data.Contains(context))
                    {
                        removed.Add(context);
                        return false;
                    }
                    return true;
                }
            }
        }

        public SearchProvider(IEnumerable<TData> initialData)
        {
            InitialData = initialData;
        }

        protected IEnumerable<TData> InitialData { get; }

        public IEnumerator<ISearchContext<TData>> GetEnumerator() => new Enumerator(InitialData);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}