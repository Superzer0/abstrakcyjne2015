using Objects.Interfaces;
using Objects.ProductionMover;

namespace ProductionLineMover.Services
{
    internal class DefaultConstructionRecipeCreator : IConstructionRecipeCreator
    {
        //Default values
        private ConstructionRecipe _constructionRecipe = new ConstructionRecipe { NameOfObject = "Fiat126p" };
        private uint _numberOfElementsToProduce = 1;

        public ConstructionRecipe ConstructionRecipe
        {
            get { return _constructionRecipe; }
            set { _constructionRecipe = value; }
        }

        public uint NumberOfElementsToProduce
        {
            get { return _numberOfElementsToProduce; }
            set { _numberOfElementsToProduce = value; }
        }
    }
}
