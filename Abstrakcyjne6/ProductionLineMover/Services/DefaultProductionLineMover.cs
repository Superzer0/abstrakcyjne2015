using System;
using Objects.Interfaces;
using Objects.ProductionMover;

namespace ProductionLineMover.Services
{
    internal class DefaultProductionLineMover : IProductionLineMover
    {
        public bool MoveProductionLine(MovingDirection movingDirection)
        {
            switch (movingDirection)
            {
                case MovingDirection.Forward:
                    Console.WriteLine("Production Moved Forward");
                    break;
                case MovingDirection.ToScran:
                    Console.WriteLine("Production Moved To Scran");
                    break;
                case MovingDirection.Back:
                    Console.WriteLine("Production Moved Back");
                    break;
            }

            return true;
        }
    }
}
