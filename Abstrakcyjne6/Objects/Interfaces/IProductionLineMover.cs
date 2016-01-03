using Objects.ProductionMover;

namespace Objects.Interfaces
{
    public interface IProductionLineMover
    {
        bool MoveProductionLine(MovingDirection movingDirection);
    }
}
