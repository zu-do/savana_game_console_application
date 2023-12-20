namespace Savanna.Models
{
    public class AnimalPair
    {
        public IAnimalPlugin Animal1 { get; }
        public IAnimalPlugin Animal2 { get; }
        public int RoundsTogether { get; set; }
        public bool UpdatedThisRound { get; set; }

        public AnimalPair(IAnimalPlugin animal1, IAnimalPlugin animal2) 
        { 
            this.Animal1 = animal1;
            this.Animal2 = animal2;
            this.RoundsTogether = 1;
            this.UpdatedThisRound = true;
        }

        public bool CheckForBirth()
        {
            if(RoundsTogether == 3)
            {
                return true;
            }

            return false;
        }

        public void AddRound() 
        { 
            this.RoundsTogether++; 
            this.UpdatedThisRound=true;
        }

    }
}
