using System;
using System.Collections;
using System.Collections.Generic;

namespace Abstrakcyjne3.Objects
{
    public class TripleList<T> : IEnumerable<T>, ICloneable, IComparable<TripleList<T>>  
        where T : IComparable<T>
    {
        public TripleList<T> PreviousElement { get; private set; }
        public TripleList<T> MiddleElement { get; private set; }
        public TripleList<T> NextElement { get; private set; }

        /// <summary>
        ///   _tripleListCommonProperties stores information common for all nodes
        /// </summary>
        private TripleListParentProperties<T> _tripleListCommonProperties;
        
        private bool _hasValue;
        private T _value;
        private bool _isMiddle;

        /// <summary>
        /// Holds list item value
        /// </summary>
        public T Value
        {
            get { return _value; }
            private set
            {
                _hasValue = true;
                _value = value;
            }
        }

        public TripleList()
        {
            _tripleListCommonProperties = new TripleListParentProperties<T>();
        }

        private TripleList(T value, TripleListParentProperties<T> tripleListParentProperties)
            : base()
        {
            _tripleListCommonProperties = tripleListParentProperties;
            Value = value;
        }


        /// <summary>
        /// Adds new element to TripleList
        /// </summary>
        /// <param name="value">Must be of T or TripleList<T> type</param>
        public void Add(object value)
        {
            if (value is T)
                Add((T)value);
            else if (value is TripleList<T>)
            {
                var newTripleList = (TripleList<T>)value;
                var tripleList = newTripleList.ToList();
                foreach (var tripleItem in tripleList)
                {
                    tripleItem._tripleListCommonProperties = _tripleListCommonProperties;
                    Add(tripleItem);
                }
            }
            else
            {
                throw new ArgumentException("Type not supported: " + value);
            }
        }

        public void Add(T value)
        {
            if (!_hasValue)
            {
                _tripleListCommonProperties.ElementsCount++;
                if (_tripleListCommonProperties.Tail == null) _tripleListCommonProperties.Tail = this;
                Value = value;
                return;
            }

            Add(new TripleList<T>(value, _tripleListCommonProperties));
        }

        /// <summary>
        ///  Returns triple list elements count
        /// </summary>
        public int Count()
        {
            return _tripleListCommonProperties.ElementsCount;
        }

        /// <summary>
        /// Returns copy of tripleList elements in list
        /// </summary>
        public List<TripleList<T>> ToList()
        {
            var treeEnumerator = GetTreeEnumerator();
            var list = new List<TripleList<T>>();

            while (treeEnumerator.MoveNext())
            {
                list.Add(treeEnumerator.Current.CloneTripleList());
            }

            return list;
        }

        public object Clone()
        {
            var cloned = (TripleList<T>)MemberwiseClone();
            ResetTripleList(cloned);
            return cloned;
        }

        public int CompareTo(TripleList<T> other)
        {
            return other == null ? 1 : Value.CompareTo(other.Value);
        }


        public IEnumerator<T> GetEnumerator()
        {
            var treeEnumerator = GetTreeEnumerator();
            while (treeEnumerator.MoveNext())
            {
                yield return treeEnumerator.Current.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerator<TripleList<T>> GetTreeEnumerator()
        {
            var pointer = this;

            while (pointer != null)
            {
                yield return pointer;
                pointer = pointer._isMiddle
                    ? pointer.MiddleElement.NextElement
                    : pointer.MiddleElement;
            }
        }

        private void Add(TripleList<T> newTripleList)
        {
            newTripleList._isMiddle = !_tripleListCommonProperties.Tail._isMiddle;

            if (newTripleList._isMiddle)
            {
                _tripleListCommonProperties.Tail.MiddleElement = newTripleList;
                newTripleList.MiddleElement = _tripleListCommonProperties.Tail;
            }
            else
            {
                _tripleListCommonProperties.Tail.MiddleElement.NextElement = newTripleList;
                newTripleList.PreviousElement = _tripleListCommonProperties.Tail.MiddleElement;
            }

            _tripleListCommonProperties.Tail = newTripleList;
            _tripleListCommonProperties.ElementsCount++;
        }

        /// <summary>
        /// Reseting TripeList object to 1 item list
        /// </summary>
        private static void ResetTripleList(TripleList<T> triple)
        {
            triple.MiddleElement = null;
            triple.NextElement = null;
            triple.PreviousElement = null;
            triple._tripleListCommonProperties = new TripleListParentProperties<T>();
            if (triple._hasValue)
                triple._tripleListCommonProperties.ElementsCount = 1;
        }

        private TripleList<T> CloneTripleList()
        {
            return (TripleList<T>)Clone();
        }
    }
}
