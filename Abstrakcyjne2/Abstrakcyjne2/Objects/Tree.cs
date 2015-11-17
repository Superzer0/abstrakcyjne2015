using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Abstrakcyjne2.Services;

namespace Abstrakcyjne2.Objects
{
    public class Tree<T> : IEnumerable<T>
    {
        private readonly List<Tree<T>> _internalNodes;
        private EnumeratorOrder _order;

        public T Value { get; set; }

        public EnumeratorOrder Order
        {
            get { return _order; }
            set
            {
                if (!Enum.IsDefined(typeof(EnumeratorOrder), value))
                {
                    throw new ArgumentOutOfRangeException();
                }

                _order = value;
                if (Children == null) return;
                foreach (var child in Children)
                {
                    child.Order = _order;
                }
            }
        }

        public Tree(T value, EnumeratorOrder order, IEnumerable<T> children = null)
        {
            Value = value;
            Order = order;
            _internalNodes = new List<Tree<T>>();

            if (children == null || !children.Any()) return;
            _internalNodes.AddRange(children.Select(p => new Tree<T>(p, order)));
        }

        public void Add(Tree<T> child)
        {
            child.Order = Order;
            _internalNodes.Add(child);
        }

        public void Add(T child)
        {
            _internalNodes.Add(new Tree<T>(child, Order));
        }

        public IEnumerable<Tree<T>> Children
        {
            get
            {
                return new ReadOnlyCollection<Tree<T>>(
                    _internalNodes != null ? _internalNodes.ToList() : new List<Tree<T>>());
            }
        }

        public IEnumerable<T> GetElements()
        {
            return TreeScanFactory.GetTreeScanMethod(Order).Scan(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetElements().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        public void Add(object item)
        {
            if (item is Tree<T>)
            {
                Add((Tree<T>)item);
            }
            else if (item is T)
            {
                Add((T)item);
            }
            else
            {
                throw new ArgumentException("Item type is not supported", "item");
            }
        }
    }
}
