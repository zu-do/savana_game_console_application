using Savanna.Core;
using AnimalBehavior;
using Savanna.Models;

namespace Savanna.Console
{
    public class Game
    {
        FieldLogic FieldLogic;
        FieldMap FieldMap;

        private readonly IOutputHandler _outputHandler;

        public Game(IOutputHandler outputHandler)
        {
            this._outputHandler = outputHandler;
            this.FieldMap = new FieldMap(SavannaConstants.Width, SavannaConstants.Height);
            IAnimalMovementManager animalMovementManager = new AnimalMovementManager(FieldMap);
            this.FieldLogic = new FieldLogic(new AnimalFactory(), animalMovementManager, new AnimalPairManager());
        }

        public void RunGame()
        {
            int roundNo = 0;

            while(true)
            {
                roundNo++;
                _outputHandler.DisplayMenu(roundNo);
                _outputHandler.DisplayField(FieldLogic, FieldMap.Width, FieldMap.Height);

                if (System.Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = System.Console.ReadKey(true);

                    char typedChar = Char.ToUpper(keyInfo.KeyChar);
                    IAnimalPlugin newAnimal = this.FieldLogic.CreateAnimal(typedChar);


                    if (newAnimal == null)
                    {
                        System.Console.WriteLine("Invalid key. Please try again.");
                    }
                }
                //if (System.Console.KeyAvailable)
                //{
                //    ConsoleKeyInfo keyInfo = System.Console.ReadKey(true);

                //    if (keyInfo.Key == ConsoleKey.A)
                //    {
                //        this.FieldLogic.CreateAnimal("A");
                //    }
                //    else if (keyInfo.Key == ConsoleKey.L)
                //    {
                //        this.FieldLogic.CreateAnimal("L");
                //    }
                //    else if (keyInfo.Key == ConsoleKey.E)
                //    {
                //        this.FieldLogic.CreateAnimal("E");
                //    }
                //}

                FieldLogic.MoveAnimals();
                
                Thread.Sleep(2000);
                System.Console.Clear();
            }
        }
    }
}
