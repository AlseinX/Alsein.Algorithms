using System.Collections.Generic;
using Alsein.Algorithms.Searching.SearchAlgorithms;

namespace Alsein.Algorithms.Searching
{
    public abstract class SearchAlgorithm
    {
        public static SearchAlgorithm DepthFirst { get; } = new DepthFirstSearchAlgorithm();
        public static SearchAlgorithm BreadthFirst { get; } = new BreadthFirstSearchAlgorithm();
        internal abstract IEnumerable<ISearchContext<TData>> GetContextSupplier<TData>(IEnumerable<TData> initals) where TData : class;
    }
}