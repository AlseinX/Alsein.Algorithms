using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Alsein.Algorithms.Searching.SearchAlgorithms;
using Alsein.Utilities;

namespace Alsein.Algorithms.Searching
{
    public abstract class SearchAlgorithm
    {
        public static SearchAlgorithm DepthFirst { get; } = new DepthFirstSearchAlgorithm();
        public static SearchAlgorithm BreadthFirst { get; } = new BreadthFirstSearchAlgorithm();
        internal abstract IEnumerable<ISearchContext<TData>> GetContextSupplier<TData>(IEnumerable<TData> initals) where TData : class;
    }
}