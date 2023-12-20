using Savanna.Models;
using AnimalAttributes;
using System.Xml.Linq;

namespace Savanna.Core
{
    public class AnimalFactory : IAnimalFactory
    {
        public IAnimalPlugin CreateAnimal(char typeChar)
        {
            Type targetType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t =>
                {
                    if (!t.IsAbstract && typeof(IAnimalPlugin).IsAssignableFrom(t))
                     {
                        var instance = (IAnimalPlugin)Activator.CreateInstance(t);
                        return instance?.CharRepresentation == typeChar;
                     }
                     return false;
                })!;

            if (targetType != null)
            {
                return (IAnimalPlugin)Activator.CreateInstance(targetType)!;
            }
            else
            {
                throw new ArgumentException("No matching class found for the specified character.");
            }
        }
    }
}
