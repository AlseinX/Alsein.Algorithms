using System.Collections.Generic;
using System.Linq;

namespace Alsein.Algorithms.Searching
{
    internal class SearchContext<TData> : ISearchContext<TData> where TData : class
    {
        public TData Data { get; }

        public ISearchContext<TData> Parent => _parent;

        public IEnumerable<ISearchContext<TData>> KnownCildren => _children.AsEnumerable();

        private ISearchEnumerator<TData> Enumerator { get; }

        public int Depth { get; }

        private SearchContext<TData> _parent;

        private IList<SearchContext<TData>> _children;

        public SearchContext(TData data, ISearchEnumerator<TData> enumerator, SearchContext<TData> parent = null)
        {
            Data = data;
            Enumerator = enumerator;
            _parent = parent;
            _children = new List<SearchContext<TData>>();
            _parent?._children.Add(this);
            Depth = parent?.Depth + 1 ?? 0;
        }

        private void Unchain()
        {
            _parent?._children.Remove(this);
            _parent = null;
        }

        public void AddContext(TData contextData) => Enumerator.Add(new SearchContext<TData>(contextData, Enumerator, this));

        public void RemoveContext(ISearchContext<TData> context)
        {
            if (Enumerator.Remove(context))
            {
                (context as SearchContext<TData>)?.Unchain();
            }
        }

        public void Break() => Enumerator.Break();
    }
}