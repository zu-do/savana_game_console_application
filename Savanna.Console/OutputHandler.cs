using Savanna.Models;
using Savanna.Core;
using AnimalAttributes;

namespace Savanna.Console
{
    public class OutputHandler : IOutputHandler
    {
        private readonly IConsoleWrapper _console;
        public OutputHandler(IConsoleWrapper consoleWrapper)
        {
            this._console = consoleWrapper;
        }

        public void DisplayMenu(int roundNo)
        {
            _console.WriteLine(SavannaConstants.WelcomeMessage);
            _console.WriteLine(SavannaConstants.SelectOptionPrompt);
            PluginLoader.Plugins.ForEach(plugin => { _console.WriteLine("To add " + plugin.ToString() + " press: " + plugin.CharRepresentation.ToString()); });
            _console.WriteLine("ROUND: " + roundNo.ToString());
        }

        public void DisplayAnimals(List<IAnimalPlugin> allAnimals)
        {
            foreach (IAnimalPlugin animal in  allAnimals)
            {
                _console.Write(animal.ToString() + " ");
                _console.WriteLine(animal.Health.ToString());
            }
        }

        public void DisplayField(IFieldLogic fieldLogic, int width, int height)
        {
            var allAnimals = fieldLogic.GetAnimals();

            this.DisplayAnimals(allAnimals);

            for (int j = 0; j < width; j++)
            {
                for (int i = 0; i < height; i++)
                {
                    bool animalPresent = false;

                    foreach (var animal in allAnimals)
                    {
                        if (animal.X == i && animal.Y == j)
                        {
                            _console.Write(animal.GetType().Name[0] + " ");
                            animalPresent = true;
                            break;
                        }
                    }

                    if (!animalPresent)
                    {
                        _console.Write("_ ");
                    }
                }
                _console.WriteLine("");
            }
            try
            {
                _console.SetCursorPosition(0, System.Console.CursorTop - height - 3 - allAnimals.Count);
            }
            catch (IOException ex)
            {
                _console.WriteLine("An error occurred while setting the cursor position: " + ex.Message);
            }
        }
    }
}
