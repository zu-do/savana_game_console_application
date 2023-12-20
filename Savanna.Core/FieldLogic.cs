using AnimalBehavior;
using Savanna.Models;

namespace Savanna.Core
{
    public class FieldLogic : IFieldLogic
    {
        IAnimalFactory _animalFactory;

        IAnimalMovementManager _movementManager;

        IAnimalPairManager _pairManager;

        private List<IAnimalPlugin> Animals = new List<IAnimalPlugin>();
        private List<IAnimalPlugin> NewBorns = new List<IAnimalPlugin>();

        public FieldLogic(IAnimalFactory animalFactory, IAnimalMovementManager movementManager, IAnimalPairManager pairManager)
        {
            this._animalFactory = animalFactory;
            this._movementManager = movementManager;
            this._pairManager = pairManager;
        }

        public IAnimalPlugin CreateAnimal(char type)
        {
            IAnimalPlugin newAnimal = this._animalFactory.CreateAnimal(type);
            this.AddAnimal(newAnimal);

            return newAnimal;
        }
        public void AddAnimal(IAnimalPlugin animal)
        {
            this.Animals.Add(animal);
        }


        public List<IAnimalPlugin> GetAnimals()
        {
            return this.Animals;
        }

        public IAnimalPlugin GetAnimal(int x, int y)
        {
            foreach (IAnimalPlugin animal in this.Animals)
            {
                if (animal.X == x && animal.Y == y)
                {
                    return animal;
                }
            }
            return null;
        }

        public void AddNewBornsToTheField()
        {
            foreach(IAnimalPlugin newBorn in this.NewBorns)
            {
                this.Animals.Add(newBorn);
                _movementManager.SetInitialPosition(newBorn);
            }
            this.NewBorns.Clear();
        }

        public void MoveAnimals()
        {
            foreach (var animal in this.Animals)
            {
                if (animal.IsNew)
                {
                    _movementManager.SetInitialPosition(animal);
                    animal.IsNew = false;
                }
                else if (animal.IsAlive)
                {
                    this.SetNextAnimalPosition(animal);
                }
                animal.Health -= 0.5;

                if(animal.Health == 0)
                {
                    animal.Die();
                }
            }
            this.RemoveDeadAnimals();
            this.UpdatePopulation();
        }

        public void UpdatePopulation()
        {
            _pairManager.MarkNewRound();

            foreach(var animal in this.Animals)
            {
                List<IAnimalPlugin> partners = this.FindSameTypeAnimalsNearby(animal);

                foreach (var partner in partners)
                {
                    AnimalPair existingPair = this._pairManager.GetPair(animal, partner);

                    if (existingPair != null && !existingPair.UpdatedThisRound)
                    {
                        existingPair.AddRound();

                        if(existingPair.CheckForBirth())
                        {
                            IAnimalPlugin newAnimal = _pairManager.Birth(existingPair);
                            this.NewBorns.Add(newAnimal);
                            _movementManager.SetInitialPosition(animal);
                            animal.IsNew = false;
                        }
                    }
                    else if(existingPair == null)
                    {
                        _pairManager.MakePair(animal, partner);
                    }
                }
            }

            _pairManager.RemoveUnupdatedPairs();
            this.AddNewBornsToTheField();
        }

        public void RemoveDeadAnimals()
        {
            List<IAnimalPlugin> animalsToRemove = new List<IAnimalPlugin>();

            foreach (var animal in this.Animals)
            {
                if (!animal.IsAlive)
                {
                    animalsToRemove.Add(animal);
                }
            }

            foreach (var animalToRemove in animalsToRemove)
            {
                this.Animals.Remove(animalToRemove);
            }
        }

        public List<IAnimalPlugin> FindSameTypeAnimalsNearby(IAnimalPlugin animal)
        {
            List<IAnimalPlugin> animalsFound = new List<IAnimalPlugin>();

            foreach(var otherAnimal in this.Animals)
            {
                bool res = _movementManager.ChekcIfAnimalsAreNear(animal, otherAnimal, 1);
                if (_movementManager.ChekcIfAnimalsAreNear(animal, otherAnimal, 1) && animal.GetType() == otherAnimal.GetType() && otherAnimal != animal)
                {
                    animalsFound.Add(otherAnimal);
                }
            }
            return animalsFound;
        }

        public List<IAnimalPlugin> FindOtherAnimalsInVisionRange(IAnimalPlugin animal)
        {
            List<IAnimalPlugin> animalsFound = new List<IAnimalPlugin>();

            for (int i = animal.X - animal.VisionRange; i <= animal.X + animal.VisionRange; i++)
            {
                for (int j = animal.Y - animal.VisionRange; j <= animal.Y + animal.VisionRange; j++)
                {
                    if (i >= 0 && i < _movementManager.GetWidth() && j >= 0 && j < _movementManager.GetHeight())
                    {
                        if (!_movementManager.IsPositionEmpty(i, j))
                        {
                            IAnimalPlugin foundAnimal = this.GetAnimal(i, j);
                            if (foundAnimal != null && foundAnimal != animal)
                            {
                                animalsFound.Add(foundAnimal);
                            }
                        }
                    }
                }
            }
            return animalsFound;
        }

        public void SetNextAnimalPosition(IAnimalPlugin animal)
        {
            List<IAnimalPlugin> allAnimalsInVisionRange = this.FindOtherAnimalsInVisionRange(animal);

            if (!animal.IsPredator)
            {
                var predatorsInVisionRange = allAnimalsInVisionRange
                    .Where(animal => animal.IsPredator == true)
                    .ToList();

                if (predatorsInVisionRange.Count > 0)
                {
                    IAnimalPlugin predator = _movementManager.FindFurthestPredatorInVisionRange(predatorsInVisionRange, animal);
                    this.AvoidPredators(predator, animal);
                }
                else
                {
                    _movementManager.MoveAnimalRandomly(animal);
                }

            }
            else if (animal.IsPredator)
            {
                var preysInVisionRange = allAnimalsInVisionRange
                    .Where(animal => animal.IsPredator == false)
                    .ToList();

                if (preysInVisionRange.Count > 0)
                {
                    IAnimalPlugin prey = _movementManager.FindClosestPreyInVisionRange(preysInVisionRange, animal);
                    if (prey != null)
                    {
                        this.CatchPrey(prey, animal);
                    }
                }
                else
                {
                    _movementManager.MoveAnimalRandomly(animal);
                }
            }
        }

        public void CatchPrey(IAnimalPlugin prey, IAnimalPlugin predator)
        {
            _movementManager.MarkPositionEmpty(predator.X, predator.Y);
            this.AnimalFeed(predator, prey);
        }

        public void AnimalFeed(IAnimalPlugin predator, IAnimalPlugin prey)
        {
            predator.X = prey.X;
            predator.Y = prey.Y;
            prey.IsAlive = false;
            predator.Health += 20;
        }

        public void AvoidPredators(IAnimalPlugin predator, IAnimalPlugin prey)
        {
            List<(int x, int y)> possibleMoves = _movementManager.GetPossibleMoveCoordinates(prey);
            double maxDist = 0;
            int x = prey.X;
            int y = prey.Y;

            foreach (var possibleMove in possibleMoves)
            {
                double currentMax = _movementManager.CalculateDistance(possibleMove.x, possibleMove.y, predator.X, predator.Y);

                if (currentMax > maxDist && _movementManager.IsPositionEmpty(possibleMove.x, possibleMove.y))
                {
                    maxDist = currentMax;
                    x = possibleMove.x;
                    y = possibleMove.y;
                }
            }

            _movementManager.MarkPositionEmpty(prey.X, prey.Y);
            prey.X = x;
            prey.Y = y;
            _movementManager.MarkPositionOccupied(x, y);
        }

    }
}
