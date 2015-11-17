using System.Collections.Generic;
using Abstrakcyjne2.Objects;

namespace Abstrakcyjne2.Interfaces
{
    public interface IScanTree
    {
        IEnumerable<T> Scan<T>(Tree<T> t);
    }
}
