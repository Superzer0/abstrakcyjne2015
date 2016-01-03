using System;
using FunctionComposition;

namespace Abstrakcyjne4
{
    class Program
    {
        static void Main(string[] args)
        {
            IExecute<int> identity = new SingleFunction<int>(x => x);
            IExecute<int> linearfunction = new SingleFunction<int>(x => 3 * x - 2);
            IExecute<int> quadraticfunction = new SingleFunction<int>(x => 2 * x * x - 5);
            IExecute<int> cubicfunction = new SingleFunction<int>(x => x * x * x + x * x + x + 1);

            var functionList = new[] { identity, linearfunction, quadraticfunction, cubicfunction };

            var comp = new Composition<int>(functionList);

            Console.WriteLine(comp.Execute(5));
            Console.ReadKey();
        }
    }
}
