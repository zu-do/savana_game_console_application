using Savanna.Models;
using System.Globalization;
using System.Reflection;

namespace AnimalAttributes
{
    public class PluginLoader
    {
        public static List<IAnimalPlugin> Plugins { get; set; }

        public void LoadPlugins(string directory)
        {
            Plugins = new List<IAnimalPlugin>();

            //Load the DLLs from the Plugins directory
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            Type interfaceType = typeof(IAnimalPlugin);
            //Fetch all types that implement the interface IPlugin and are a class
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                .ToArray();
            foreach (Type type in types)
            {
                //Create a new instance of all found types
                Plugins.Add((IAnimalPlugin)Activator.CreateInstance(type));
            }
        }
    }
}
