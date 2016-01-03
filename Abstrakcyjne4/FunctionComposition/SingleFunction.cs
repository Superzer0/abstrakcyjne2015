using System;
using System.Collections;
using System.Collections.Generic;

namespace FunctionComposition
{
    /// <summary>
    ///  Behaves as leaf in composite pattern
    /// </summary>
    public class SingleFunction<T> : IExecute<T>
    {
        private readonly Func<T, T> _functionToExecute;

        public SingleFunction(Func<T,T> functionToExecute)
        {
            _functionToExecute = functionToExecute;
        }

        public T Execute(T arg)
        {
            return _functionToExecute(arg);
        }

        public void Add(IExecute<T> command)
        {
            // Leaf does not support this operation
        }

        public IEnumerator<IExecute<T>> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
