using System.Collections.Generic;
using System.Linq;
using Abstrakcyjne2.Interfaces;
using Abstrakcyjne2.Objects;

namespace Abstrakcyjne2.Services.TreeScan
{
    internal class BfsScan : IScanTree
    {
        public IEnumerable<T> Scan<T>(Tree<T> t)
        {
            var returnList = new List<T>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(t);

            while (queue.Count != 0)
            {
                var tree = queue.Dequeue();
                returnList.Add(tree.Value);
                var descendants = tree.Children;
                if (descendants == null || !descendants.Any()) continue;

                foreach (var subTree in descendants)
                {
                    queue.Enqueue(subTree);
                }
            }

            return returnList;
        }
    }
}
