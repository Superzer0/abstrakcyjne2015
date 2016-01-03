using System;
using Autofac;
using Objects.Interfaces;
using ProductionLineMover;

namespace Abstrakcyjne6
{
    class Program
    {
        static void Main(string[] args)
        {
            var diContainer = Di.GetContainerScope;
            var templateMethod = diContainer.Resolve<ControllerTemplateMethod>();
            var recipeCreator = diContainer.Resolve<IConstructionRecipeCreator>();

            recipeCreator.NumberOfElementsToProduce = 2;
            recipeCreator.ConstructionRecipe.NameOfObject = "Nowe auto";

            templateMethod.Execute();

            Console.ReadKey();
        }
    }
}
