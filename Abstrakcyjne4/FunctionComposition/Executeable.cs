using System.Collections.Generic;

namespace FunctionComposition
{
    public interface IExecute<T> : IEnumerable<IExecute<T>>
    {
        T Execute(T arg);
        void Add(IExecute<T> command);
    }
}
