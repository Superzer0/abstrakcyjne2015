using System;
using Objects.Logger;
using Objects.ProductionMover;

namespace ProductionLineMover
{
    public abstract class ControllerTemplateMethod
    {
        public void Execute()
        {
            for (var i = 0; i < GetNumberOfElementsToProduct(); i++)
            {
                try
                {
                    var recipe = ConstructRecipe();
                    var nameOfObject = recipe.NameOfObject;

                    MoveProduction(MovingDirection.Forward);

                    if (ConstructObject(recipe))
                    {
                        LogWork(LoggingType.Info, "Produced" + nameOfObject);
                        MoveProduction(MovingDirection.Forward);
                    }
                    else
                    {
                        LogWork(LoggingType.Warning, "Not Produced: " + nameOfObject);
                        MoveProduction(MovingDirection.ToScran);
                    }
                }
                catch (Exception e)
                {
                    LogWork(LoggingType.Error, "Production Error" + e.Message); ;
                }
            }
        }

        protected abstract ConstructionRecipe ConstructRecipe();
        protected abstract uint GetNumberOfElementsToProduct();
        protected abstract bool MoveProduction(MovingDirection direction);
        protected abstract bool ConstructObject(ConstructionRecipe recipe);
        protected abstract void LogWork(LoggingType type, string message);
    }
}
