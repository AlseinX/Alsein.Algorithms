using System.Collections.Generic;

namespace Alsein.Algorithms.Searching
{
    public interface ISearchContext<TData> where TData : class
    {
        TData Data { get; }
        ISearchContext<TData> Parent { get; }
        IEnumerable<ISearchContext<TData>> KnownCildren { get; }
        int Depth { get; }
        void AddContext(TData contextData);
        void RemoveContext(ISearchContext<TData> context);
        void Break();
    }
}