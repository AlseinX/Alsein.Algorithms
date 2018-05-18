using System.Collections.Generic;

namespace Alsein.Algorithms.Searching
{
    internal interface ISearchEnumerator<TData> : IEnumerator<ISearchContext<TData>> where TData : class
    {
        void Add(ISearchContext<TData> context);
        bool Remove(ISearchContext<TData> context);
        void Break();
    }
}