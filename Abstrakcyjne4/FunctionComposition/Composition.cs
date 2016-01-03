using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FunctionComposition
{
    /// <summary>
    ///  Behaves as Composite in composite pattern
    /// </summary>
    public class Composition<T> : IExecute<T>
    {
        private readonly List<IExecute<T>> _leafsToExecute = new List<IExecute<T>>();

        public Composition(params IExecute<T>[] commands)
        {
            if (commands != null && commands.Any())
                _leafsToExecute.AddRange(commands);
        }

        public T Execute(T arg)
        {
            if (!_leafsToExecute.Any())
                throw new InvalidOperationException("Composition cannot be empty");

            return _leafsToExecute
                .Aggregate(arg, (current, command) => command.Execute(current));
        }

        public void Add(IExecute<T> command)
        {
            _leafsToExecute.Add(command);
        }

        public IEnumerator<IExecute<T>> GetEnumerator()
        {
            return _leafsToExecute
                 .Aggregate(new List<IExecute<T>>(), (accumulator, elem) =>
                 {
                     accumulator.AddRange(elem.ToList());
                     return accumulator;
                 }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
