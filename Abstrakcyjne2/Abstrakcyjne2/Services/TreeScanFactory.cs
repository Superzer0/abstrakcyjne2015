using System.Collections.Generic;
using Abstrakcyjne2.Interfaces;
using Abstrakcyjne2.Objects;
using Abstrakcyjne2.Services.TreeScan;

namespace Abstrakcyjne2.Services
{
    public static class TreeScanFactory
    {
        public static IScanTree GetTreeScanMethod(EnumeratorOrder order)
        {
            return Dictionary[order];
        }

        private static readonly Dictionary<EnumeratorOrder, IScanTree> Dictionary
            = new Dictionary<EnumeratorOrder, IScanTree>
            {
                  {EnumeratorOrder.BreadthFirstSearch, new BfsScan()},
                  {EnumeratorOrder.DepthFirstSearch, new DfsScan()}
            };
    }
}
