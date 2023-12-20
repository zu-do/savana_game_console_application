namespace Savanna.Console
{
    public interface IConsoleWrapper
    {
        void Clear();
        string ReadLine();
        void Write(string message);
        void WriteLine(string message);
        ConsoleKeyInfo ReadKey(bool intercept);
        void SetCursorPosition(int left, int top);
    }
}