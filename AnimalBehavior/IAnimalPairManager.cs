using Savanna.Models;

namespace AnimalBehavior
{
    public interface IAnimalPairManager
    {
        IAnimalPlugin Birth(AnimalPair pair);
        List<AnimalPair> GetAllPairs();
        AnimalPair GetPair(IAnimalPlugin animal1, IAnimalPlugin animal2);
        void MakePair(IAnimalPlugin animal1, IAnimalPlugin animal2);
        void MarkNewRound();
        void RemoveUnupdatedPairs();
    }
}