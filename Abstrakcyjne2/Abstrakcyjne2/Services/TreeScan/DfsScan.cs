using System.Collections.Generic;
using System.Linq;
using Abstrakcyjne2.Interfaces;
using Abstrakcyjne2.Objects;

namespace Abstrakcyjne2.Services.TreeScan
{
    internal class DfsScan : IScanTree
    {
        public IEnumerable<T> Scan<T>(Tree<T> t)
        {
            var returnList = new List<T>();
            Scan(t, returnList);
            return returnList;
        }

        private void Scan<T>(Tree<T> tree, List<T> accumulator)
        {
            accumulator.Add(tree.Value);

            var descendants = tree.Children;
            if (descendants == null || !descendants.Any()) return;

            foreach (var subTree in descendants)
            {
                Scan(subTree, accumulator);
            }
        }
    }
}
