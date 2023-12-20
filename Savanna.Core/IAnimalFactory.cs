using Savanna.Models;

namespace Savanna.Core
{
    public interface IAnimalFactory
    {
        IAnimalPlugin CreateAnimal(char type);
    }
}