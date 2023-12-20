using AnimalAttributes;
using Savanna.Console;

try
{
    PluginLoader loader = new PluginLoader();
    loader.LoadPlugins(SavannaConstants.FolderName);
}
catch (Exception e)
{
    Console.WriteLine(string.Format("Plugins couldn't be loaded: {0}", e.Message));
}


IConsoleWrapper consoleWrapper = new ConsoleWrapper();
IOutputHandler outputHandler = new OutputHandler(consoleWrapper);

Game game = new Game(outputHandler);

game.RunGame();