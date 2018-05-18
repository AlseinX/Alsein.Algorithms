using System;
using Alsein.Algorithms.Searching;
using System.Linq;

namespace Alsein.Algorithms.Test
{
    class Test
    {
        public int Value { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var depth = 0;
            var found = false;
            var result = new[] { new Test { Value = 1 } }.Search(SearchAlgorithm.BreadthFirst).AsParallel().Where(x =>
            {
                if (x.Depth > depth)
                {
                    if (found)
                    {
                        x.Break();
                        return false;
                    }
                    depth = x.Depth;
                }

                if (x.Data.Value == 48)
                {
                    return found = true;
                }

                x.AddContext(new Test { Value = x.Data.Value * 2 });
                x.AddContext(new Test { Value = x.Data.Value * 3 });
                return false;
            });

            foreach (var item in result)
            {
                Console.WriteLine($"{item.Data} is found with path {string.Join("/", item.GetPath())}");
            }


            foreach (var item in result)
            {
                Console.WriteLine($"{item.Data} is found with path {string.Join("/", item.GetPath())}");
            }
        }
    }
}
