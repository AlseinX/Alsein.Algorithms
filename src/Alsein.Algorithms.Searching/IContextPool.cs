using System.Collections.Generic;

namespace Alsein.Algorithms.Searching
{
    internal interface IContextPool<TData> where TData : class
    {
        void Add(ISearchContext<TData> context);
        bool TryFetch(out ISearchContext<TData> context);
        bool Contains(ISearchContext<TData> context);
    }
}