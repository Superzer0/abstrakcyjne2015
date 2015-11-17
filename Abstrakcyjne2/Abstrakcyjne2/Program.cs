using System;
using Abstrakcyjne2.Objects;

namespace Abstrakcyjne2
{
    class Program
    {
        static void Main(string[] args)
        {
            // add some tree usage here 

            var subtree = new Tree<int>(5, EnumeratorOrder.DepthFirstSearch) { 1, 2 };
            var tree = new Tree<int>(7, EnumeratorOrder.DepthFirstSearch) { subtree, 10, 15 };
            var result = tree.GetElements();

            foreach (var item in result)
            {
                Console.Write(item + ", ");
            }

            Console.ReadKey();
        }
    }
}
