using Objects.ProductionMover;

namespace Objects.Interfaces
{
    public interface IConstructionRecipeCreator
    {
        ConstructionRecipe ConstructionRecipe { get; set; }
        uint NumberOfElementsToProduce { get; set; }
    }
}
