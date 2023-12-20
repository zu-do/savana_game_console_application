using AnimalBehavior;
using Savanna.Core;
using Savanna.Models;

namespace Savanna.Tests
{
    public class PairManagerTests
    {
        private readonly IAnimalPairManager _manager;

        public PairManagerTests()
        {
            _manager = new AnimalPairManager();
        }

        [Fact]
        public void MakePair_ShouldCreateNewPair()
        {
            IAnimalPlugin lion1 = new Lion();
            IAnimalPlugin lion2 = new Lion();

            _manager.MakePair(lion1, lion2);
            List<AnimalPair> pairs = _manager.GetAllPairs();

            Assert.Single(pairs);
            Assert.Equal(lion1, pairs[0].Animal1);
            Assert.Equal(lion2, pairs[0].Animal2);
        }

        [Fact]
        public void Birth_ShouldCreateNewAnimal()
        {
            IAnimalPlugin lion1 = new Lion();
            IAnimalPlugin lion2 = new Lion();
            _manager.MakePair(lion1, lion2);
            AnimalPair pair = _manager.GetAllPairs()[0];

            IAnimalPlugin newAnimal = _manager.Birth(pair);

            Assert.NotNull(newAnimal);
            if (lion1 is Lion && lion2 is Lion)
            {
                Assert.IsType<Lion>(newAnimal);
            }
            else
            {
                Assert.IsType<Antelope>(newAnimal);
            }
        }

    }
}
