using Savanna.Models;

namespace Savanna.Core
{
    public interface IFieldLogic
    {
        void AddAnimal(IAnimalPlugin animal);
        void AvoidPredators(IAnimalPlugin predator, IAnimalPlugin prey);
        void CatchPrey(IAnimalPlugin prey, IAnimalPlugin predator);
        IAnimalPlugin CreateAnimal(char type);
        List<IAnimalPlugin> FindOtherAnimalsInVisionRange(IAnimalPlugin animal);
        IAnimalPlugin GetAnimal(int x, int y);
        List<IAnimalPlugin> GetAnimals();
        List<IAnimalPlugin> FindSameTypeAnimalsNearby(IAnimalPlugin animal);
        void AddNewBornsToTheField();
        void MoveAnimals();
        void RemoveDeadAnimals();
        void SetNextAnimalPosition(IAnimalPlugin animal);
    }
}