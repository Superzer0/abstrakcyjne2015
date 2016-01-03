using System;
using Objects.Interfaces;
using Objects.ProductionMover;

namespace ProductionLineMover.Services
{
    internal class DefaultObjectsConstructor : IObjectsConstructor
    {
        public bool ConstructObjectFromRecipe(ConstructionRecipe recipe)
        {
            Console.WriteLine("Working hard to produce " + recipe.NameOfObject);
            return true;
        }
    }
}
