using System;
using System.Collections.Generic;
using System.Linq;
using Alsein.Utilities;

namespace Alsein.Algorithms.Searching
{
    public static class SearchingExtensions
    {
        public static IEnumerable<ISearchContext<TData>> Search<TData>(this IEnumerable<TData> data, SearchAlgorithm algorithm = null) where TData : class
        {
            if (algorithm == null)
            {
                algorithm = SearchAlgorithm.BreadthFirst;
            }
            var result = algorithm.GetContextSupplier<TData>(data);
            return result;
        }

        public static IEnumerable<TData> GetPath<TData>(this ISearchContext<TData> context) where TData : class => context.Recurse(x => x.Parent).Reverse().Select(x => x.Data);
    }
}