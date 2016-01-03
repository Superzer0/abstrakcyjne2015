using Objects.ProductionMover;

namespace Objects.Interfaces
{
    public interface IObjectsConstructor
    {
        bool ConstructObjectFromRecipe(ConstructionRecipe recipe);
    }
}
