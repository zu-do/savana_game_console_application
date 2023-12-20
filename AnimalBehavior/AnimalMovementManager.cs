using Savanna.Models;
using Savanna;

namespace AnimalBehavior
{
    public class AnimalMovementManager : IAnimalMovementManager
    {
        FieldMap Field;

        public AnimalMovementManager (FieldMap field) 
        {
            this.Field = field;
        }

        public bool IsPositionEmpty(int x, int y)
        {
            return Field.IsPositionEmpty(x, y);
        }

        public void MarkPositionOccupied(int x, int y)
        {
            Field.MarkPositionOccupied(x, y);
        }

        public void MarkPositionEmpty(int x, int y)
        {
            Field.MarkPositionEmpty(x, y);
        }

        public int GetWidth()
        {
            return Field.Width;
        }

        public int GetHeight()
        {
            return Field.Height;
        }


        public void MoveSpecificLocation(IAnimalPlugin animal, int x, int y)
        {
            this.MarkPositionEmpty(animal.X, animal.Y);
            animal.X = x;
            animal.Y = y;
            this.MarkPositionOccupied(x, y);
        }

        public void SetInitialPosition(IAnimalPlugin animal)
        {
            int attempts = 0;
            Random random = new Random();
            do
            {
                int x = random.Next(0, this.GetWidth());
                int y = random.Next(0, this.GetHeight());

                if (this.IsPositionEmpty(x, y))
                {
                    animal.X = x;
                    animal.Y = y;
                    this.MarkPositionOccupied(animal.X, animal.Y);
                    return;
                }

                attempts++;
            }
            while (attempts < 100);
        }

        public double CalculateDistance(int x1, int y1, int x2, int y2)
        {
            double distanceSquared = Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2);
            return Math.Sqrt(distanceSquared);
        }

        public List<(int x, int y)> GetPossibleMoveCoordinates(IAnimalPlugin animal)
        {
            List<(int x, int y)> possibleCoordinates = new List<(int x, int y)>();

            for (int i = Math.Max(0, animal.X - animal.VisionRange); i <= Math.Min(this.GetWidth() - 1, animal.X + animal.VisionRange); i++)
            {
                for (int j = Math.Max(0, animal.Y - animal.VisionRange); j <= Math.Min(this.GetHeight() - 1, animal.Y + animal.VisionRange); j++)
                {
                    if (i != animal.X || j != animal.Y)
                    {
                        possibleCoordinates.Add((i, j));
                    }
                }
            }

            return possibleCoordinates;
        }

        public IAnimalPlugin FindFurthestPredatorInVisionRange(List<IAnimalPlugin> predators, IAnimalPlugin prey)
        {
            double maxDistance = 0;
            IAnimalPlugin closestAnimal = null;

            foreach (IAnimalPlugin a in predators)
            {
                double currentDistance = CalculateDistance(prey.X, prey.Y, a.X, a.Y);
                if (currentDistance > maxDistance)
                {
                    maxDistance = currentDistance;
                    closestAnimal = a;
                }
            }

            return closestAnimal;
        }

        public IAnimalPlugin FindClosestPreyInVisionRange(List<IAnimalPlugin> preys, IAnimalPlugin predator)
        {
            double minDistance = double.MaxValue;
            IAnimalPlugin closestAnimal = null;

            foreach (IAnimalPlugin a in preys)
            {
                double currentDistance = CalculateDistance(predator.X, predator.Y, a.X, a.Y);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestAnimal = a;
                }
            }

            return closestAnimal;
        }

        public void MoveAnimalRandomly(IAnimalPlugin animal)
        {
            List<(int x, int y)> possibleMoves = this.GetPossibleMoveCoordinates(animal);

            int attempts = 0;

            do
            {
                if (possibleMoves.Count > 0)
                {
                    Random random = new Random();
                    int randomIndex = random.Next(0, possibleMoves.Count);

                    (int newX, int newY) = possibleMoves[randomIndex];

                    if (this.IsPositionEmpty(newX, newY))
                    {
                        this.MoveSpecificLocation(animal, newX, newY);
                        return;
                    }
                }

                attempts++;
            }
            while (attempts < 100);
        }

        public bool ChekcIfAnimalsAreNear(IAnimalPlugin first, IAnimalPlugin second, int proximity)
        {
            int deltaX = Math.Abs(first.X - second.X);
            int deltaY = Math.Abs(first.Y - second.Y);

            int maxDelta = Math.Max(deltaX, deltaY);

            return maxDelta <= proximity;
        }
    }
}
