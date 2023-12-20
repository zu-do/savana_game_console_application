using AnimalBehavior;
using Moq;
using Savanna.Core;
using Savanna.Models;

namespace Savanna.Tests
{
    public class FieldLogicTests
    {
        private readonly IFieldLogic fieldLogic;

        private readonly Mock<IAnimalFactory> factory;
        private readonly Mock<IAnimalPairManager> pair;
        private readonly IAnimalMovementManager movement;
        public FieldLogicTests()
        {
            this.factory = new Mock<IAnimalFactory>();
            this.movement = new AnimalMovementManager(new FieldMap(5,5));
            this.pair = new Mock<IAnimalPairManager>();
            this.fieldLogic = new FieldLogic(factory.Object, movement, pair.Object);
        }

        [Fact]
        public void FindNearbySameTypeAnimals_ShouldReturnCorrectList()
        {
            List<IAnimalPlugin> lions = new List<IAnimalPlugin>
            {
                new Lion { X = 2, Y = 2 },
                new Lion { X = 4, Y = 4 },
                new Lion { X = 0, Y = 0 },
                new Lion { X = 1, Y = 1 },
                new Antelope { X = 4, Y = 4 }
            };

            fieldLogic.AddAnimal(lions[0]);
            fieldLogic.AddAnimal(lions[1]);
            fieldLogic.AddAnimal(lions[2]);
            fieldLogic.AddAnimal(lions[3]);
            fieldLogic.AddAnimal(lions[4]);

            Lion lion = new Lion { X = 0, Y = 1 };

            List<IAnimalPlugin> result = fieldLogic.FindSameTypeAnimalsNearby(lion);

            Assert.Contains(lions[2], result);
            Assert.Contains(lions[3], result);
        }
    }
}
