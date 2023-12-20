using Savanna.Models;

namespace AnimalBehavior
{
    public class AnimalPairManager : IAnimalPairManager
    {
        private List<AnimalPair> Pairs;

        public AnimalPairManager()
        {
            this.Pairs = new List<AnimalPair>();
        }

        public List<AnimalPair> GetAllPairs()
        {
            return this.Pairs;
        }
        public void MakePair(IAnimalPlugin animal1, IAnimalPlugin animal2)
        {
            AnimalPair newPair = new AnimalPair(animal1, animal2);
            this.Pairs.Add(newPair);
        }

        public AnimalPair GetPair(IAnimalPlugin animal1, IAnimalPlugin animal2)
        {
            foreach (AnimalPair pair in Pairs)
            {
                if ((pair.Animal1 == animal1 && pair.Animal2 == animal2) ||
                    (pair.Animal1 == animal2 && pair.Animal2 == animal1))
                {
                    return pair;
                }
            }

            return null;
        }

        public IAnimalPlugin Birth(AnimalPair pair)
        {
            this.Pairs.Remove(pair);

            Type type1 = pair.Animal1.GetType();
            Type type2 = pair.Animal2.GetType();

            if (type1 == type2)
            {
                return (IAnimalPlugin)Activator.CreateInstance(type1);
            }
            else
            {
                throw new Exception("Unsupported pair types");
            }
        }

        public void MarkNewRound()
        {
            foreach(AnimalPair pair in this.Pairs)
            {
                pair.UpdatedThisRound = false;
            }
        }

        public void RemoveUnupdatedPairs()
        {
            this.Pairs.RemoveAll(pair => !pair.UpdatedThisRound);
        }

    }
}
