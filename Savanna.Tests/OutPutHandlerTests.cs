using AnimalBehavior;
using Moq;
using Savanna.Console;
using Savanna.Core;
using Savanna.Models;

namespace Savanna.Tests
{
    public class OutPutHandlerTests
    {
        private readonly OutputHandler outputHandler;
        private readonly Mock<IConsoleWrapper> console;
        
        public OutPutHandlerTests() 
        { 
            this.console = new Mock<IConsoleWrapper>();
            outputHandler = new OutputHandler(console.Object);
        }

        [Fact]
        public void DisplayMenu_ShouldDisplayWelcomeMessageAndSelectOptionPrompt()
        {
            int round = 2;

            outputHandler.DisplayMenu(round);

            console.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Exactly(3));
            console.Verify(c => c.WriteLine(SavannaConstants.WelcomeMessage), Times.Once);
            console.Verify(c => c.WriteLine(SavannaConstants.SelectOptionPrompt), Times.Once);
        }

        [Fact]
        public void DisplayField_ShouldDisplayFieldCorrectly()
        {
            var fieldLogicMock = new Mock<IFieldLogic>();
            var managerMock = new Mock<IAnimalMovementManager>();
            managerMock.Setup(f => f.GetHeight()).Returns(3);
            managerMock.Setup(f => f.GetWidth()).Returns(4);

            var animals = new List<IAnimalPlugin>
            {
            new Antelope { X = 1, Y = 1 },
            new Lion { X = 2, Y = 2 }
            };

            fieldLogicMock.Setup(f => f.GetAnimals()).Returns(animals);

            outputHandler.DisplayField(fieldLogicMock.Object, managerMock.Object.GetHeight(), managerMock.Object.GetWidth());

           
            console.Verify(c => c.Write(It.IsAny<string>()), Times.Exactly(14));
            console.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Exactly(6));
        }
    }
}