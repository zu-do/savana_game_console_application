using Savanna.Core;

namespace Savanna.Console
{
    public interface IOutputHandler
    {
        void DisplayField(IFieldLogic fieldLogic, int width, int height);
        void DisplayMenu(int roundNo);
    }
}