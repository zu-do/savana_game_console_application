using AnimalBehavior;
using Moq;
using Savanna.Core;
using Savanna.Models;

namespace Savanna.Tests
{
    public class MovementManagerTests
    {
        private readonly IAnimalMovementManager _manager;
        public MovementManagerTests()
        {
            var field = new Mock<FieldMap>(5, 5);

            this._manager = new AnimalMovementManager(field.Object);
        }

        [Fact]
        public void SetInitialPosition_ShouldSetValidCoordinates()
        {
            
            var animal = new Antelope { VisionRange = 2 };
            var width = 5;
            var height = 5; 

            _manager.SetInitialPosition(animal);

            Assert.InRange(animal.X, 0, width - 1);
            Assert.InRange(animal.Y, 0, height - 1);
        }

        [Fact]
        public void GetPossibleMoveCoordinates_ShouldReturnCorrectCoordinates()
        {
            var animal = new Lion { X = 2, Y = 2 };

            var width = 5;
            var height = 5;


            var result = _manager.GetPossibleMoveCoordinates(animal);

            Assert.Contains((1, 1), result);
            Assert.Contains((2, 1), result);
            Assert.Contains((1, 2), result);
            Assert.Contains((3, 1), result);
            Assert.Contains((1, 3), result);
            Assert.Contains((2, 3), result);
            Assert.Contains((3, 2), result);
            Assert.Contains((3, 3), result);

            Assert.Equal(8, result.Count);
        }

        [Theory]
        [InlineData(4, 4)]
        [InlineData(3, 1)]
        public void MoveSpecificLocation_ShouldSetCorectCoordinates(int x, int y)
        {
            var animal = new Antelope { VisionRange = 2 };

            _manager.MoveSpecificLocation(animal, x, y);

            Assert.True(!_manager.IsPositionEmpty(x, y));

            Assert.Equal(x, animal.X);
            Assert.Equal(y, animal.Y);
        }

        [Theory]
        [InlineData(6, 4)]
        [InlineData(3, 7)]
        public void MoveSpecificLocationOutsideField_ShouldThrowOutOfBoundEx(int x, int y) 
        {
            var animal = new Antelope { VisionRange = 2 };

            Assert.Throws<ArgumentOutOfRangeException>(() => _manager.MoveSpecificLocation(animal, x, y));
        }

        [Fact]
        public void FindFurthestPredatorInVisionRange_ShouldReturnNullIfLionsListIsEmpty()
        {
            List<IAnimalPlugin> lions = new List<IAnimalPlugin>();
            IAnimalPlugin antelope = new Antelope { X = 1, Y = 1 };

            IAnimalPlugin result = _manager.FindFurthestPredatorInVisionRange(lions, antelope);

            Assert.Null(result);
        }

        [Fact]
        public void FindFurthestPredatorInVisionRange_ShouldReturnCorrectFurthestLion()
        {
            List<IAnimalPlugin> lions = new List<IAnimalPlugin>
            {
                new Lion { X = 2, Y = 2 },
                new Lion { X = 4, Y = 4 },
                new Lion { X = 5, Y = 5 }
            };
            IAnimalPlugin antelope = new Antelope { X = 1, Y = 1 };

            IAnimalPlugin result = _manager.FindFurthestPredatorInVisionRange(lions, antelope);

            Assert.NotNull(result);
            Assert.Equal(5, result.X);
            Assert.Equal(5, result.Y);
        }

        [Fact]
        public void FindClosestPreyInVisionRange_ShouldReturnNullIfAntelopesListIsEmpty()
        {
            List<IAnimalPlugin> antelopes = new List<IAnimalPlugin>();
            IAnimalPlugin lion = new Lion { X = 1, Y = 1 };

            IAnimalPlugin result = _manager.FindClosestPreyInVisionRange(antelopes, lion);

            Assert.Null(result);
        }

        [Fact]
        public void FindClosestPreyInVisionRange_ShouldReturnCorrectClosestAntelope()
        {
            List<IAnimalPlugin> antelopes = new List<IAnimalPlugin>
            {
                new Antelope { X = 2, Y = 2 },
                new Antelope { X = 4, Y = 4 },
                new Antelope { X = 1, Y = 1 }
            };

            IAnimalPlugin lion = new Lion { X = 0, Y = 0 };

            IAnimalPlugin result = _manager.FindClosestPreyInVisionRange(antelopes, lion);

            Assert.NotNull(result);
            Assert.Equal(1, result.X);
            Assert.Equal(1, result.Y);
        }
    }
}
