using Objects.Interfaces;
using Objects.Logger;
using Objects.ProductionMover;

namespace ProductionLineMover
{
    public class ControllerWithDependencyInjection : ControllerTemplateMethod
    {
        private readonly IObjectsConstructor _objectsConstructor;
        private readonly IConstructionRecipeCreator _constructionRecipeCreator;
        private readonly ILogger _logger;
        private readonly IProductionLineMover _productionLineMover;

        public ControllerWithDependencyInjection(IObjectsConstructor objectsConstructor,
            IConstructionRecipeCreator constructionRecipeCreator, ILogger logger,
            IProductionLineMover productionLineMover)
        {
            _objectsConstructor = objectsConstructor;
            _constructionRecipeCreator = constructionRecipeCreator;
            _logger = logger;
            _productionLineMover = productionLineMover;
        }

        protected override ConstructionRecipe ConstructRecipe()
        {
            return _constructionRecipeCreator.ConstructionRecipe;
        }

        protected override uint GetNumberOfElementsToProduct()
        {
            return _constructionRecipeCreator.NumberOfElementsToProduce;
        }

        protected override bool MoveProduction(MovingDirection direction)
        {
            return _productionLineMover.MoveProductionLine(direction);
        }

        protected override bool ConstructObject(ConstructionRecipe recipe)
        {
            return _objectsConstructor.ConstructObjectFromRecipe(recipe);
        }

        protected override void LogWork(LoggingType type, string message)
        {
            _logger.Log(type, message);
        }
    }
}
