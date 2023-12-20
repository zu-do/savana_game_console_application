using Savanna.Models;

namespace AnimalBehavior
{
    public interface IAnimalMovementManager
    {
        int GetHeight();
        int GetWidth();
        void MarkPositionEmpty(int x, int y);
        void MarkPositionOccupied(int x, int y);
        bool IsPositionEmpty(int x, int y);
        void MoveSpecificLocation(IAnimalPlugin animal, int x, int y);
        void MoveAnimalRandomly(IAnimalPlugin animal);
        void SetInitialPosition(IAnimalPlugin animal);
        double CalculateDistance(int x1, int y1, int x2, int y2);
        List<(int x, int y)> GetPossibleMoveCoordinates(IAnimalPlugin animal);
        IAnimalPlugin FindClosestPreyInVisionRange(List<IAnimalPlugin> preys, IAnimalPlugin predator);
        IAnimalPlugin FindFurthestPredatorInVisionRange(List<IAnimalPlugin> predators, IAnimalPlugin prey);
        bool ChekcIfAnimalsAreNear(IAnimalPlugin first, IAnimalPlugin second, int proximity);
    }
}