using System;

namespace Abstrakcyjne3.Objects
{
    internal sealed class TripleListParentProperties<T> where T : IComparable<T>
    {
        public int ElementsCount { get; set; }
        public TripleList<T> Tail { get; set; }
    }
}
